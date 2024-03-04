using API.Data;
using API.Models;
using API.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   [Authorize]
   public class FavoritesController : BaseApiController
   {
      private readonly FoodieContext _context;
      public FavoritesController(FoodieContext context)
      {
         _context = context;

      }

      [HttpGet(Name = "GetFavorites")]
      public async Task<ActionResult<Favorites>> GetFavorites()
      {
         Favorites favorites = await RetrieveFavorites(GetUserId());

         if (favorites == null) {
            //Todo implement getting cookie data
            return NotFound();
         };

         return Ok(favorites);
      }

      [HttpPost("AddRecipe")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([BindRequired][FromQuery]FavoriteRecipeParams favoriteRecipeParams)
      {
         var favorites = await InitializeFavorites(GetUserId());

         var recipe = await _context.Recipes.FindAsync(favoriteRecipeParams.RecipeId);
         if (recipe == null) return NotFound();

         favorites.AddFavoriteRecipe(recipe);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
      }

      [HttpPost("AddRestaurant")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRestaurant([BindRequired][FromQuery]FavoriteRestaurantParams favoriteRecipeParams)
      {
         var favorites = await InitializeFavorites(GetUserId());

         var restaurant = await _context.Restaurants.FindAsync(favoriteRecipeParams.RestaurantId);
         if (restaurant == null) return NotFound();

         favorites.AddFavoriteRestaurant(restaurant);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
      }

      private string GetUserId()
      {
         return User.Identity?.Name ?? Request.Cookies["buyerId"];
      }

      private async Task<Favorites> InitializeFavorites(string userId)
      {
         var favorites = await RetrieveFavorites(userId);
         if (favorites == null) favorites = CreateFavorites();

         return favorites;
      }

      private async Task<Favorites> RetrieveFavorites(string userId)
      {
         if (string.IsNullOrEmpty(userId))
         {
            Response.Cookies.Delete("userId");
            return null;
         }

         return await _context.Favorites
                  .Include(b => b.Recipes)
                  .ThenInclude(r => r.Recipe)
                  .Include(f => f.Restaurants)
                  .ThenInclude(r => r.Restaurant)
                  .FirstOrDefaultAsync(rec => rec.UserId == userId);
      }

      private Favorites CreateFavorites()
      {
         var userId = User.Identity?.Name;
         if (string.IsNullOrEmpty(userId))
         {
            userId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("userId", userId, cookieOptions);
         }

         var favorites = new Favorites { UserId = userId };
         // _context.Favorites.Add(favorites); //To be removed when user authentication is resolved

         return favorites;
      }
   }
}
