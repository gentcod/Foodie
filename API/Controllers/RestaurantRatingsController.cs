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
[ApiController]
[Route("api/v1/restaurants/ratings")]
public class RestaurantRatingsController(FoodieContext context) : ControllerBase
{
    private readonly FoodieContext _context = context;

    [HttpGet(Name = "restaurantRating")]
    public async Task<ActionResult> GetRestaurantRatings([FromQuery] PaginationParams paginationParams)
    {
        var query = _context.RestaurantRatings.Include(r => r.Restaurant).Where(rat => rat.TotalRatings > 0).AsQueryable();
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
        if (restaurantRatings == null || !restaurantRatings.Ratings.Any()) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant ratings not found"
        ));

        var restaurantRatingsDto = restaurantRatings.MapRestaurantRatingsToDto(restaurant);

        return Ok(ApiSuccessResponse<RestaurantRatingsDto>.Response(
            "success",
            "Restaurant ratings fetched successfully",
            restaurantRatingsDto
        ));
    }

    [Authorize]
    [HttpPost("add/{restaurantId}")]
    public async Task<ActionResult> AddRating([BindRequired]RatingDto ratingDto, [BindRequired][FromRoute] int restaurantId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User account not found"
        ));

        if (ratingDto.RatingNum < 1 || ratingDto.RatingNum > 5) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Rating number is out of range"
        ));

        var restaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (restaurant == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings
            .Include(el => el.Ratings)
            .FirstOrDefaultAsync(el => el.RestaurantId == restaurantId);

        if (restaurantRatings != null && restaurantRatings.Ratings != null)
        {
            var existingRating = restaurantRatings.Ratings.FirstOrDefault(el => el.UserId == GetUserId());
            if (existingRating != null && restaurantRatings.RestaurantId == restaurantId) return BadRequest(ApiErrorResponse.Response(
                    "error",
                    "Restaurant rating has been previously added"
                ));
        }

        restaurantRatings ??= new RestaurantRatings();      
        restaurantRatings.AddRating(new Rating
        {
            User = user,
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        restaurant.RestaurantRatings = restaurantRatings;
        var newRatingNum = restaurantRatings.Ratings.Average(rating => rating.RatingNum);
        restaurant.RatingNum = newRatingNum;

        _context.Restaurants.Update(restaurant);
        var result = await _context.SaveChangesAsync() > 0;
        if (result) 
        {
            var response = ApiSuccessResponse<RestaurantRatingsDto>.Response(
                "success",
                "Restaurant rating has been added successfully",
                restaurantRatings.MapRestaurantRatingsToDto(restaurant)
            );
            return CreatedAtRoute("restaurantRating", response);
        }

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding Restaurant Rating"
        ));
    }


    [Authorize]
    [HttpDelete("remove/{restaurantId}")]
    public async Task<ActionResult> Remove([BindRequired][FromRoute] int restaurantId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User account not found"
        ));

        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(el => el.Id == restaurantId);
        if (restaurant == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings
            .Include(el => el.Ratings)
            .FirstOrDefaultAsync(el => el.RestaurantId == restaurant.Id);
        if (restaurantRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant rating not found"
        ));

        if (restaurantRatings.Ratings != null)
        {
            var existingRating = restaurantRatings.Ratings.FirstOrDefault(el => el.UserId == GetUserId());
            if (existingRating == null) return NotFound(ApiErrorResponse.Response(
                    "error",
                    "User rating not found"
                ));
        }

        var removedRating = restaurantRatings.RemoveRating(GetUserId());

        double newRatingNum = restaurantRatings.Ratings.Any() ? 
            restaurantRatings.Ratings.Average(rating => rating.RatingNum)
            :0;
        restaurant.RatingNum = newRatingNum;
        restaurant.RestaurantRatings = restaurantRatings;

        _context.Restaurants.Update(restaurant);
        _context.Ratings.Remove(removedRating);
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
    
    private Guid GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        _ = Guid.TryParse(userIdClaim.Value, out var userId);
        return userId;
    }
}