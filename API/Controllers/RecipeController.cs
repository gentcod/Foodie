using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class RecipeController : BaseApiController
   {
      private readonly FoodieContext _context;
      public RecipeController(FoodieContext context)
      {
         _context = context;
      }
      
      [HttpGet]
      public async Task<ActionResult<Recipe>> GetRecipes()
      {
        var recipes = await _context.Recipes.ToListAsync();

        return Ok(recipes);
      }
   }
}