using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Models;
using API.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class FavoritesController(FoodieContext context) : BaseApiController
{
   private readonly FoodieContext _context = context;

   [HttpGet(Name = "GetFavorites")]
   public async Task<ActionResult> GetFavorites()
   {
      Favorites favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "No favorites found"
      ));

      IEnumerable<Favorites> enumerable = [favorites];
      var favoritesResult = enumerable.AsQueryable();
      var data = favoritesResult.MapFavoritesToDto();

      return Ok(ApiSuccessResponse<IQueryable<FavoritesDto>>.Response(
         "success",
         "Favorites have been fetched successfully",
         data
      ));
   }

   [HttpPost("add/recipe")]
   public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([BindRequired][FromQuery] FavoriteRecipeParams favRecParams)
   {
      var favorites = await InitializeFavorites(GetUserId());

      var recipe = await _context.Recipes.FindAsync(favRecParams.RecipeId);
      if (recipe == null) return BadRequest(ApiErrorResponse.Response(
         "error",
         "Recipe not found"
      ));

      if (favorites.Recipes != null)
      {
         var existingFavRecipe = favorites.Recipes.FirstOrDefault(rec => rec.RecipeId == recipe.Id);
         if (existingFavRecipe != null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe has been previously added to Favorites"
         ));
      }

      favorites.AddFavoriteRecipe(recipe);
      var result = _context.SaveChangesAsync();
      if (result != null)
      {
         IEnumerable<Favorites> enumerable = [favorites];
         var favoritesResult = enumerable.AsQueryable();
         var data = favoritesResult.MapFavoritesToDto();

         var response = ApiSuccessResponse<IQueryable<FavoritesDto>>.Response(
            "success",
            "Recipe has been added to Favorites successfully",
            data
         );
         return CreatedAtRoute("GetFavorites", response);
      }

      return BadRequest(ApiErrorResponse.Response(
         "error",
         "Problem adding recipe to Favorite"
      ));
   }

   [HttpPost("add/restaurant")]
   public async Task<ActionResult> AddNewFavoriteRestaurant([BindRequired][FromQuery] FavoriteRestaurantParams favResParams)
   {
      var favorites = await InitializeFavorites(GetUserId());

      var restaurant = await _context.Restaurants.FindAsync(favResParams.RestaurantId);
      if (restaurant == null) return BadRequest(ApiErrorResponse.Response(
         "error",
         "Restaurant not found"
      ));

      if (favorites.Restaurants != null)
      {
         var existingFavREstaurant = favorites.Restaurants.FirstOrDefault(rec => rec.RestaurantId == restaurant.Id);
         if (existingFavREstaurant != null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Restaurant has been previously added to Favorites"
         ));
      }

      favorites.AddFavoriteRestaurant(restaurant);

      var result = _context.SaveChangesAsync();
      if (result != null)
      {
         IEnumerable<Favorites> enumerable = [favorites];
         var favoritesResult = enumerable.AsQueryable();
         var data = favoritesResult.MapFavoritesToDto();

         var response = ApiSuccessResponse<IQueryable<FavoritesDto>>.Response(
            "success",
            "Restaurant has been added to Favorites successfully",
            data
         );
         return CreatedAtRoute("GetFavorites", response);
      }

      return BadRequest(ApiErrorResponse.Response(
         "error",
         "Problem adding restaurant to Favorite"
      ));
   }


   [HttpPost("remove/recipe")]
   public async Task<ActionResult> RemoveRecipe([BindRequired][FromQuery] FavoriteRecipeParams favRecParams)
   {
      var favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null) return NotFound(ApiErrorResponse.Response(
          "error",
          "No favorites found"
      ));

      var recipe = favorites.Recipes.FirstOrDefault(rec => rec.RecipeId == favRecParams.RecipeId);
      if (recipe == null) return NotFound(
          ApiErrorResponse.Response(
             "error",
             "Recipe is not in favorites"
          )
      );

      favorites.RemoveFavoriteRecipe(favRecParams.RecipeId);
      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         IEnumerable<Favorites> enumerable = [favorites];
         var favoritesResult = enumerable.AsQueryable();
         var data = favoritesResult.MapFavoritesToDto();

         return Ok(
         ApiSuccessResponse<IQueryable<FavoritesDto>>.Response(
             "success",
             "Favorite has been removed successfully",
             data
         )
     );
      }

      return BadRequest(
          ApiErrorResponse.Response(
              "error",
              "Problem adding Favorite"
          )
      );
   }

   [HttpPost("remove/restaurant")]
   public async Task<ActionResult> RemoveRestaurant([BindRequired][FromQuery] FavoriteRestaurantParams favResParams)
   {
      var favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null) return NotFound(ApiErrorResponse.Response(
          "error",
          "No favorites found"
      ));

      var recipe = favorites.Restaurants.FirstOrDefault(rec => rec.RestaurantId == favResParams.RestaurantId);
      if (recipe == null) return NotFound(
          ApiErrorResponse.Response(
             "error",
             "Restaurant is not in favorites"
          )
      );

      favorites.RemoveFavoriteRestaurant(favResParams.RestaurantId);
      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         IEnumerable<Favorites> enumerable = [favorites];
         var favoritesResult = enumerable.AsQueryable();
         var data = favoritesResult.MapFavoritesToDto();

         return Ok(
         ApiSuccessResponse<IQueryable<FavoritesDto>>.Response(
             "success",
             "Favorite has been removed successfully",
             data
         )
     );
      }

      return BadRequest(
          ApiErrorResponse.Response(
              "error",
              "Problem adding Favorite"
          )
      );
   }

   private string GetUserId()
   {
      var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
      return userIdClaim.Value;
   }

   private async Task<Favorites> InitializeFavorites(string userId)
   {
      var favorites = await RetrieveFavorites(userId) ?? CreateFavorites(userId);
      return favorites;
   }

   private async Task<Favorites> RetrieveFavorites(string userId)
   {
      return await _context.Favorites
               .Include(b => b.Recipes)
               .ThenInclude(r => r.Recipe)
               .Include(f => f.Restaurants)
               .ThenInclude(r => r.Restaurant)
               .FirstOrDefaultAsync(rec => rec.UserId == userId);
   }

   private Favorites CreateFavorites(string userId)
   {
      var favorites = new Favorites { UserId = userId };
      _context.Favorites.Add(favorites);
      return favorites;
   }
}
