using API.Data;
using API.DTOs;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using API.RequestHelpers;

namespace API.Controllers;
public class RestaurantController(FoodieContext context) : BaseApiController
{
    private readonly FoodieContext _context = context;

    [HttpGet(Name = "GetRestaurants")]
    public async Task<ActionResult> GetRestaurants([FromQuery]PaginationParams paginationParams)
    {
        var restaurants = _context.Restaurants.Include(el => el.Geolocation).AsQueryable();

        var restaurantDto = restaurants.MapRestaurantsToDto();
        var paginatedResponse = await PagedList<RestaurantDto>.ToPagedList(restaurantDto, paginationParams.PageNumber, paginationParams.PageSize);

        Response.AddPaginationHeader(paginatedResponse.MetaData);

        return Ok(ApiSuccessResponse<PagedList<RestaurantDto>>.Response(
            "success",
            "Recipes fetched successfully",
            paginatedResponse
        ));
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult> GetRestaurantById([BindRequired] int restaurantId)
    {
        var restaurant = await _context.Restaurants.Include(el => el.Geolocation)
              .FirstOrDefaultAsync(res => res.Id == restaurantId);

        if (restaurant == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "Restaurant not found"
        ));

        return Ok(ApiSuccessResponse<RestaurantDto>.Response(
            "success",
            "Restaurant fetched successfully",
            restaurant.MapRestaurantToDto()
         ));
    }
}