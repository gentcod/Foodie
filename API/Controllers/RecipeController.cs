using API.Data;
using API.Models;
using API.RequestHelpers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;

namespace API.Controllers
{
   public class RecipeController : BaseApiController
   {
      private readonly FoodieContext _context;
      public RecipeController(FoodieContext context)
      {
         _context = context;
      }
      
      [HttpGet(Name = "GetRecipes")]
      public async Task<ActionResult<RecipeDto>> GetRecipes([FromQuery] RecipeParams recipeParams)
      {
         var query = _context.Recipes
         .Search(recipeParams.Keyword)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .AsQueryable();

      
        var recipes = await query.ToListAsync();

        return Ok(recipes.MapRecipesToDto());
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
      {
         var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == id);

         if (recipe == null) return NotFound();

         return Ok(recipe.MapRecipeToDto());
      }
   }
}