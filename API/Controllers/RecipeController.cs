using API.Data;
using API.RequestHelpers;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers;
public class RecipeController(FoodieContext context) : BaseApiController
{
   private readonly FoodieContext _context = context;

   [HttpGet(Name = "GetRecipes")]
   public async Task<ActionResult> GetRecipes([FromQuery] RecipeParams recipeParams)
   {
      var query = _context.Recipes
      .Search(recipeParams.Search)
      .Sort(recipeParams.SortBy)
      .OrderByCookTime(recipeParams.OrderBy)
      .FilterByCategory(recipeParams.Category)
      .AsQueryable();

      var recipeDtos = query.MapRecipesToDto();

      var paginatedResponse = await PagedList<ListedRecipeDto>.ToPagedList(recipeDtos, recipeParams.PageNumber, recipeParams.PageSize);

      Response.AddPaginationHeader(paginatedResponse.MetaData);

      return Ok(ApiSuccessResponse<PagedList<ListedRecipeDto>>.Response(
          "success",
          "Recipes fetched successfully",
          paginatedResponse
       ));
   }

   [HttpGet("featured", Name = "featured")]
   public async Task<ActionResult> GetFeaturedRecipes()
   {
      var query = _context.Recipes.Featured().AsQueryable();

      var featuredRecipesQuery = query.MapRecipesToDto();

      var featuredRecipes = await featuredRecipesQuery.ToListAsync();

      return Ok(ApiSuccessResponse<List<ListedRecipeDto>>.Response(
         "success",
         "Featured recipes fetched successfully",
         featuredRecipes
      ));
   }

   [HttpGet("{recipeId}")]
   public async Task<ActionResult> GetRecipeById([BindRequired] int recipeId)
   {
      var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == recipeId);

      if (recipe == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "Recipe not found"
      ));

      return Ok(ApiSuccessResponse<RecipeDto>.Response(
         "success",
         "Recipe fetched successfully",
         recipe.MapRecipeToDto()
      ));
   }
}
