using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using API.RequestHelpers;
using System.Security.Claims;

namespace API.Controllers;
public class RestaurantRatingsController : BaseApiController
{
    private readonly FoodieContext _context;
    public RestaurantRatingsController(FoodieContext context)
    {
        _context = context;

    }

    [HttpGet(Name = "restaurantRating")]
    public async Task<ActionResult> GetRestaurantRatings([FromQuery] PaginationParams paginationParams)
    {
        var query = _context.RestaurantRatings.Include(r => r.Restaurant).AsQueryable();
        var restaurantsRatingsDto = query.MapRestaurantsRatingsToDto();

        var paginatedResponse = await PagedList<ListedRestaurantRatingsDto>.ToPagedList(
            restaurantsRatingsDto, 
            paginationParams.PageNumber, 
            paginationParams.PageSize
        );

        Response.AddPaginationHeader(paginatedResponse.MetaData);

        return Ok(ApiSuccessResponse<PagedList<ListedRestaurantRatingsDto>>.Response(
            "success",
            "Restaurants ratings fetched successfully",
            paginatedResponse
        ));
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult> GetRestaurantRatingsById([BindRequired] int restaurantId)
    {
        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(el => el.Id == restaurantId);
        if (restaurant == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings
            .Include(rec => rec.Ratings)
            .FirstOrDefaultAsync(el => el.RestaurantId == restaurant.Id);
        if (restaurantRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant rating not found"
        ));

        var restaurantRatingsDto = restaurantRatings.MapRestaurantRatingsToDto(restaurant);

        return Ok(ApiSuccessResponse<RestaurantRatingsDto>.Response(
            "success",
            "Restaurant ratings fetched successfully",
            restaurantRatingsDto
        ));
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<Restaurant>> AddRating(RatingDto ratingDto, [BindRequired][FromQuery] int restaurantId)
    {
        if (ratingDto.RatingNum < 1 || ratingDto.RatingNum > 5) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Rating number is out of range"
        ));

        var restaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (restaurant == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings.FirstOrDefaultAsync(el => el.RestaurantId == restaurant.Id);
        restaurantRatings ??= new RestaurantRatings();

        restaurantRatings.AddRating(new Rating
        {
            UserId = GetUserId(),
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        restaurant.RestaurantRatings = restaurantRatings;
        restaurant.RatingNum = Math.Round(restaurantRatings.Ratings.Average(rating => rating.RatingNum), 1);

        _context.Restaurants.Update(restaurant);
        var result = _context.SaveChangesAsync();
        var response = ApiSuccessResponse<RestaurantRatingsDto>.Response(
            "success",
            "Restaurant rating has been added successfully",
            restaurantRatings.MapRestaurantRatingsToDto(restaurant)
        );
        if (result != null) return CreatedAtRoute("restaurantRating", response);

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding Restaurant Rating"
        ));
    }


    [Authorize]
    [HttpPost("remove")]
    public async Task<ActionResult> Remove([BindRequired][FromQuery] int restaurantId)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(el => el.Id == restaurantId);
        if (restaurant == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings.FirstOrDefaultAsync(el => el.RestaurantId == restaurant.Id);
        if (restaurantRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant rating not found"
        ));

        var userId = GetUserId();
        var userRating = restaurantRatings.Ratings.FirstOrDefault(rating => rating.UserId == userId);
        if (userRating == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User rating not found"
        ));

        restaurantRatings.RemoveRating(userId);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            return Ok(
                ApiSuccessResponse<RestaurantRatingsDto>.Response(
                    "success",
                    "Restaurant rating has been removed successfully",
                    restaurantRatings.MapRestaurantRatingsToDto(restaurant)
                )
            );
        }

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem removing restaurant rating"
        ));
    }
    private string GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        return userIdClaim.Value;
    }
}