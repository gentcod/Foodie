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

      [HttpGet(Name = "GetRestaurants")]
      public async Task<ActionResult<Restaurant>> GetRestaurants()
      {
         var restaurants = await _context.Restaurants.Include(el => el.Geolocation).Include(el => el.RestaurantRatings).ToListAsync();

         var restaurantsDto = restaurants.MapRestaurantsToDto();

         return Ok(restaurantsDto);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<RecipeDto>> GetRestaurantById(int id)
      {
         var restaurant = await _context.Restaurants.Include(el => el.RestaurantRatings)
               .FirstOrDefaultAsync(res => res.Id == id);

         if (restaurant == null) return NotFound();

         return Ok();
      }

      [HttpPatch("AddRestaurantRating")]
      public async Task<ActionResult<Recipe>> AddRating([FromQuery] int restaurantId, int ratingNum, string review)
      {
         if (ratingNum < 1 || ratingNum > 5) return BadRequest(new ProblemDetails { Title = "Rating number is out of rating" });

         var restaurant = await _context.Restaurants.FindAsync(restaurantId);

         if (restaurant == null) return BadRequest(new ProblemDetails { Title = "Restaurant not found" });

         if (restaurant.RestaurantRatings == null) restaurant.RestaurantRatings = new List<RatingRestaurant>();

         restaurant.RestaurantRatings.Add(new RatingRestaurant
         {
            RatingNum = ratingNum,
            Comment = review,
         });

         _context.Restaurants.Update(restaurant);
         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetRestaurants", restaurant);

         return BadRequest(new ProblemDetails { Title = "Problem adding Restaurant Rating" });
      }

   }
}