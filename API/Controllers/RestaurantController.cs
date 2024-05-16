using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;
public class RestaurantController : BaseApiController
{
   private readonly FoodieContext _context;
   public RestaurantController(FoodieContext context)
   {
      _context = context;
   }

   [HttpGet(Name = "GetRestaurants")]
   public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
   {
      var restaurants = await _context.Restaurants.Include(el => el.Geolocation).ToListAsync();

      return Ok(restaurants.MapRestaurantsToDto());
   }

   [HttpGet(":id")]
   public async Task<ActionResult<RestaurantDto>> GetRestaurantById([BindRequired][FromQuery] int restaurantId)
   {
      var restaurant = await _context.Restaurants.Include(el => el.Geolocation)
            .FirstOrDefaultAsync(res => res.Id == restaurantId);

      if (restaurant == null) return NotFound();

      return Ok(restaurant.MapRestaurantToDto());
   }
}