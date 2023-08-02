using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class RestaurantRatingsController : BaseApiController
    {
        private readonly FoodieContext _context;
        public RestaurantRatingsController(FoodieContext context)
        {
         _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<RestaurantRatingsDto>> GetRecipeRatings()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            var restaurantRatings = await _context.RestaurantRatings.ToListAsync();

            var restaurantRatingsDto = restaurantRatings.Select(rec => new RestaurantRatingsDto
            {
                RatingId = rec.Id,
                RestaurantName = restaurants.Find(el => el.Id == rec.RestaurantId).Name,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });

            return Ok(restaurantRatingsDto);
        }

    }
}