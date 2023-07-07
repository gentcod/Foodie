using API.Data;
using API.Entities;
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
      
      [HttpGet]
      public async Task<ActionResult<RecipeDto>> GetRecipes([FromQuery] RecipeParams recipeParams)
      {
         var query = _context.Recipes
         .Search(recipeParams.Keyword)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .AsQueryable();

      
        var recipes = await query.ToListAsync();

        return Ok(recipes.MapRecipeToDto());
      }
   }
}