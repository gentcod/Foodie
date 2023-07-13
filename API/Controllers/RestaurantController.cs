using API.Data;
using API.DTOs;
using API.Entities;
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

      [HttpGet]
      public async Task<ActionResult<Restaurant>> GetRestaurants()
      {
        var restaurants = await _context.Restaurants.ToListAsync();
        var bearings = await _context.Bearing.ToListAsync();

        var restaurantsDto = bearings.Join(
            restaurants,
            bearing => bearing,
            restaurant => restaurant.Geolocation,
            (bearing, restaurant) => new RestaurantDto
            {
               Id = restaurant.Id,
               Name = restaurant.Name,
               Location = restaurant.Location,
               ImgSrc = restaurant.ImgSrc,
               Geolocation = bearing,
               RatingRestaurant = restaurant.RatingRestaurant,
            }
         );

        return Ok(restaurantsDto);
      }
   }
}