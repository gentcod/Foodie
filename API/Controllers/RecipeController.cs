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
      
      [HttpGet(Name = "GetRecipes")]
      public async Task<ActionResult<RecipeDto>> GetRecipes([FromQuery] RecipeParams recipeParams)
      {
         var query = _context.Recipes
         .Search(recipeParams.Keyword)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .AsQueryable();

      
        var recipes = await query.Include(el => el.RecipeRatings).ToListAsync();

        return Ok(recipes.MapRecipesToDto());
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
      {
         var recipe = await _context.Recipes.Include(el => el.RecipeRatings)
               .FirstOrDefaultAsync(rec => rec.Id == id);

         if (recipe == null) return NotFound();

         return Ok(recipe.MapRecipeToDto());
      }

      [HttpPatch("AddRecipeRating")]
      public async Task<ActionResult<Recipe>> AddRating([FromQuery] int recipeId, int ratingNum, string review)
      {
         var recipe = await _context.Recipes.FindAsync(recipeId);

         if (recipe == null) return BadRequest(new ProblemDetails { Title = "Recipe not found" });

         if (recipe.RecipeRatings == null) recipe.RecipeRatings = new List<RatingRecipe>();
         
         recipe.RecipeRatings.Add(new RatingRecipe
         {
            RatingNum = ratingNum,
            Comment = review,
         });

         _context.Recipes.Update(recipe);
         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetRecipes", recipe);

         return BadRequest(new ProblemDetails { Title = "Problem adding Recipe Rating" });
      }
   }
}