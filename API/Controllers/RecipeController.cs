using API.Data;
using API.RequestHelpers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using API.Models;

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
      public async Task<ActionResult<PagedList<RecipeDto>>> GetRecipes([FromQuery] RecipeParams recipeParams)
      {
         var query = _context.Recipes
         .Search(recipeParams.Search)
         .Sort(recipeParams.SortBy)
         .OrderByCookTime(recipeParams.OrderBy)
         .AsQueryable();
      
        var recipeDtos = query.MapRecipesToDto();

        var pagedList = await PagedList<RecipeDto>.ToPagedList(recipeDtos, recipeParams.PageNumber, recipeParams.PageSize);

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

      [HttpGet("RecipeById")]
      public async Task<ActionResult<RecipeDto>> GetRecipeById([BindRequired][FromQuery]int id)
      {
         var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == id);

         if (recipe == null) return NotFound();

         return Ok(recipe.MapRecipeToDto());
      }
   }
}