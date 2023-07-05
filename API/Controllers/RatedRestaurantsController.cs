using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class RatedRestaurantsController: BaseApiController
    {
        private readonly FoodieContext _context;
        public RatedRestaurantsController(FoodieContext context)
        {
            _context = context;
        }

         [HttpGet]
      public async Task<ActionResult<RatedRecipes>> GetRatedRestaurant()
      {
        var ratedRestaurant = await _context.RatedRestaurants.ToListAsync();
        return Ok(ratedRestaurant);
      }
    }
}