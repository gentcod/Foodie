using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using API.RequestHelpers;

namespace API.Controllers;
public class RestaurantRatingsController : BaseApiController
{
    private readonly FoodieContext _context;
    public RestaurantRatingsController(FoodieContext context)
    {
        _context = context;

    }

    [HttpGet(Name = "restaurantRating")]
    public async Task<ActionResult> GetRestaurantRatings()
    {
        var restaurants = await _context.Restaurants.ToListAsync();
        var restaurantsRatings = await _context.RestaurantRatings.ToListAsync();

        var restaurantsRatingsDto = restaurantsRatings.MapRestaurantsRatingsToDto(restaurants);

        return Ok(ApiSuccessResponse<IEnumerable<RestaurantRatingsDto>>.Response(
            "success",
            "Recipes ratings fetched successfully",
            restaurantsRatingsDto
        ));
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult> GetRestaurantRatingsById([BindRequired] int restaurantId)
    {
        var restaurant = await _context.Restaurants.FirstOrDefaultAsync(el => el.Id == restaurantId);
        if (restaurant == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        var restaurantRatings = await _context.RestaurantRatings.Where(el => el.RestaurantId == restaurantId).ToListAsync();

        var restaurantRatingsDto = restaurantRatings.MapRestaurantRatingsToDto(restaurant);

        return Ok(ApiSuccessResponse<IEnumerable<RestaurantRatingsDto>>.Response(
            "success",
            "Recipe ratings fetched successfully",
            restaurantRatingsDto
        ));
    }

    [Authorize]
    [HttpPost("AddRating")]
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

        var restaurantRatings = await _context.RestaurantRatings.Where(el => el.RestaurantId == restaurantId).ToListAsync();
        restaurantRatings ??= new List<RestaurantRating>();

        restaurantRatings.Add(new RestaurantRating
        {
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        restaurant.Rating = Math.Round(restaurantRatings.Average(rating => rating.RatingNum), 1);
        restaurant.RestaurantRatings = restaurantRatings;

        _context.Restaurants.Update(restaurant);
        var result = _context.SaveChangesAsync();
        var response = ApiSuccessResponse<IEnumerable<RestaurantRatingsDto>>.Response(
            "success",
            "Restaurant rating added successfully",
            restaurantRatings.MapRestaurantRatingsToDto(restaurant)
        );
        if (result != null) return CreatedAtRoute("restaurantRating", response);

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding Restaurant Rating"
        ));
    }
}