using API.Data;
using API.DTOs;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using API.RequestHelpers;

namespace API.Controllers;
public class RestaurantController : BaseApiController
{
    private readonly FoodieContext _context;
    public RestaurantController(FoodieContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetRestaurants")]
    public async Task<ActionResult<PagedList<RestaurantDto>>> GetRestaurants([FromQuery]PaginationParams paginationParams)
    {
        var restaurants = _context.Restaurants.Include(el => el.Geolocation).AsQueryable();

        var restaurantDto = restaurants.MapRestaurantsToDto();
        var paginatedResponse = await PagedList<RestaurantDto>.ToPagedList(restaurantDto, paginationParams.PageNumber, paginationParams.PageSize);

        Response.AddPaginationHeader(paginatedResponse.MetaData);

        return Ok(paginatedResponse);
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurantById([BindRequired] int restaurantId)
    {
        var restaurant = await _context.Restaurants.Include(el => el.Geolocation)
              .FirstOrDefaultAsync(res => res.Id == restaurantId);

        if (restaurant == null) return NotFound();

        return Ok(restaurant.MapRestaurantToDto());
    }
}