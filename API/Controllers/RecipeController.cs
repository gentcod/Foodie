using API.Data;
using API.Models;
using API.RequestHelpers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
         .Search(recipeParams.Search)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .AsQueryable();

      
        var recipes = await query.ToListAsync();

        return Ok(recipes.MapRecipesToDto());
      }

      [HttpGet(":id")]
      public async Task<ActionResult<RecipeDto>> GetRecipeById([BindRequired][FromQuery]int recipeId)
      {
         var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == recipeId);

         if (recipe == null) return NotFound();

         return Ok(recipe.MapRecipeToDto());
      }
   }
}