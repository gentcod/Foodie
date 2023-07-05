using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class RatedRecipesController: BaseApiController
    {
        private readonly FoodieContext _context;
        public RatedRecipesController(FoodieContext context)
        {
            _context = context;
        }

         [HttpGet]
      public async Task<ActionResult<RatedRecipes>> GetRatedRecipes()
      {
        var ratedRecipes = await _context.RatedRecipes.ToListAsync();
        return Ok(ratedRecipes);
      }
    }
}