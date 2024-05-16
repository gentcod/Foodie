using API.Data;
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
      public async Task<ActionResult<PagedList<ListedRecipeDto>>> GetRecipes([FromQuery] RecipeParams recipeParams)
      {
         var query = _context.Recipes
         .Search(recipeParams.Search)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .FilterByCategory(recipeParams.Category)
         .AsQueryable();
      
        var recipeDtos = query.MapRecipesToDto();

        var pagedList = await PagedList<ListedRecipeDto>.ToPagedList(recipeDtos, recipeParams.PageNumber, recipeParams.PageSize);

        Response.AddPaginationHeader(pagedList.MetaData);

        return Ok(pagedList);
      }

      [HttpGet("featured", Name = "featured")]
      public async Task<ActionResult<RecipeDto>> GetFeaturedRecipes()
      {
         var query = _context.Recipes.Featured().AsQueryable();

         var featuredRecipesQuery = query.MapRecipesToDto();

         var featuredRecipes = await featuredRecipesQuery.ToListAsync();

         return Ok(featuredRecipes);
      }

      [HttpGet("{recipeId}")]
      public async Task<ActionResult<RecipeDto>> GetRecipeById([BindRequired]int recipeId)
      {
         var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == recipeId);

         if (recipe == null) return NotFound();

         return Ok(recipe.MapRecipeToDto());
      }
   }
}