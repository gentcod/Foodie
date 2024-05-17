using System.Security.Claims;
using API.Data;
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
   public async Task<ActionResult<Favorites>> GetFavorites()
   {
      Favorites favorites = await RetrieveFavorites(GetUserId());

      if (favorites == null)
      {
         return NotFound();
      };

      IEnumerable<Favorites> enumerable = [favorites];
      var favoritesResult = enumerable.AsQueryable();

      return Ok(favoritesResult.MapFavoritesToDto());
   }

   [HttpPost("AddRecipe")]
   public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([BindRequired][FromQuery] FavoriteRecipeParams favoriteRecipeParams)
   {
      var favorites = await InitializeFavorites(GetUserId());

      var recipe = await _context.Recipes.FindAsync(favoriteRecipeParams.RecipeId);
      if (recipe == null) return NotFound();

      if (favorites.Recipes != null)
      {
         var existingBookmark = favorites.Recipes.FirstOrDefault(rec => rec.RecipeId == recipe.Id);
         if (existingBookmark != null) return BadRequest(new ProblemDetails
         {
            Detail = "Recipe has been previously added to Favorites"
         });
      }

      favorites.AddFavoriteRecipe(recipe);

      IEnumerable<Favorites> enumerable = [favorites];
      var favoritesResult = enumerable.AsQueryable();

      var result = _context.SaveChangesAsync();
      if (result != null) return CreatedAtRoute("GetFavorites", favoritesResult.MapFavoritesToDto());

      return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
   }

   [HttpPost("AddRestaurant")]
   public async Task<ActionResult<Favorites>> AddNewFavoriteRestaurant([BindRequired][FromQuery] FavoriteRestaurantParams favoriteRecipeParams)
   {
      var favorites = await InitializeFavorites(GetUserId());

      var restaurant = await _context.Restaurants.FindAsync(favoriteRecipeParams.RestaurantId);
      if (restaurant == null) return NotFound();

      if (favorites.Recipes != null)
      {
         var existingBookmark = favorites.Restaurants.FirstOrDefault(rec => rec.RestaurantId == restaurant.Id);
         if (existingBookmark != null) return BadRequest(new ProblemDetails
         {
            Detail = "Restaurant has been previously added to Favorites"
         });
      }

      favorites.AddFavoriteRestaurant(restaurant);

      IEnumerable<Favorites> enumerable = [favorites];
      var favoritesResult = enumerable.AsQueryable();

      var result = _context.SaveChangesAsync();
      if (result != null) return CreatedAtRoute("GetFavorites", favoritesResult.MapFavoritesToDto());

      return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
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
