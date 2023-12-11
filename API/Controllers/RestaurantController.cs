using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
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

      [HttpGet("{id}")]
      public async Task<ActionResult<RestaurantDto>> GetRestaurantById(int id)
      {
         var restaurant = await _context.Restaurants.Include(el => el.Geolocation)
               .FirstOrDefaultAsync(res => res.Id == id);

         if (restaurant == null) return NotFound();

         return Ok(restaurant.MapRestaurantToDto());
      }
   }
}