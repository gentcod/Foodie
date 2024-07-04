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

      var data = favorites.MapFavoritesToDto();

      return Ok(ApiSuccessResponse<FavoritesDto>.Response(
         "success",
         "Favorites have been fetched successfully",
         data
      ));
   }

   [HttpPost("recipes/add/{recipeId}")]
   public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([BindRequired][FromRoute] int recipeId)
   {
      var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
      if (user == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "User not found"
      ));

      var favorites = await InitializeFavorites(GetUserId());

      var recipe = await _context.Recipes.FindAsync(recipeId);
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

      var errResp = favorites.AddFavoriteRecipe(recipe);
      if (errResp != null) return BadRequest(errResp);

      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         var data = favorites.MapFavoritesToDto();

         var response = ApiSuccessResponse<FavoritesDto>.Response(
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

   [HttpPost("restaurants/add/{restaurantId}")]
   public async Task<ActionResult> AddNewFavoriteRestaurant([BindRequired][FromRoute] int restaurantId)
   {
      var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
      if (user == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "User not found"
      ));
      var favorites = await InitializeFavorites(GetUserId());

      var restaurant = await _context.Restaurants.FindAsync(restaurantId);
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

      var errResp = favorites.AddFavoriteRestaurant(restaurant);
      if (errResp != null) return BadRequest(errResp);

      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         var data = favorites.MapFavoritesToDto();

         var response = ApiSuccessResponse<FavoritesDto>.Response(
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


   [HttpDelete("recipes/remove/{recipeId}")]
   public async Task<ActionResult> RemoveRecipe([BindRequired][FromRoute] int recipeId)
   {
      var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
      if (user == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "User not found"
      ));
      var favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null) return NotFound(ApiErrorResponse.Response(
          "error",
          "No favorites found"
      ));

      var recipe = favorites.Recipes.FirstOrDefault(rec => rec.RecipeId == recipeId);
      if (recipe == null) return NotFound(
          ApiErrorResponse.Response(
             "error",
             "Recipe is not in favorites"
          )
      );

      favorites.RemoveFavoriteRecipe(recipeId);
      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         var data = favorites.MapFavoritesToDto();

         return Ok(
         ApiSuccessResponse<FavoritesDto>.Response(
             "success",
             "Recipe has been removed from Favorites successfully",
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

   [HttpDelete("restaurants/remove/{restaurantId}")]
   public async Task<ActionResult> RemoveRestaurant([BindRequired][FromRoute] int restaurantId)
   {
      var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
      if (user == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "User not found"
      ));
      var favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null) return NotFound(ApiErrorResponse.Response(
          "error",
          "No favorites found"
      ));

      var recipe = favorites.Restaurants.FirstOrDefault(rec => rec.RestaurantId == restaurantId);
      if (recipe == null) return NotFound(
          ApiErrorResponse.Response(
             "error",
             "Restaurant is not in favorites"
          )
      );

      favorites.RemoveFavoriteRestaurant(restaurantId);
      var result = await _context.SaveChangesAsync() > 0;
      if (result)
      {
         var data = favorites.MapFavoritesToDto();

         return Ok(
         ApiSuccessResponse<FavoritesDto>.Response(
             "success",
             "Restaurant has been removed from Favorites successfully",
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

   private Guid GetUserId()
   {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        _ = Guid.TryParse(userIdClaim.Value, out var userId);
        return userId;
   }

   private async Task<Favorites> InitializeFavorites(Guid userId)
   {
      var favorites = await RetrieveFavorites(userId) ?? CreateFavorites(userId);
      return favorites;
   }

   private async Task<Favorites> RetrieveFavorites(Guid userId)
   {
      return await _context.Favorites
               .Include(b => b.Recipes)
               .ThenInclude(r => r.Recipe)
               .Include(f => f.Restaurants)
               .ThenInclude(r => r.Restaurant)
               .FirstOrDefaultAsync(rec => rec.UserId == userId);
   }

   private Favorites CreateFavorites(Guid userId)
   {
      var favorites = new Favorites { UserId = userId };
      _context.Favorites.Add(favorites);
      return favorites;
   }
}
