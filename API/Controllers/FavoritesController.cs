using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class FavoritesController : BaseApiController
   {
      private readonly FoodieContext _context;
      public FavoritesController(FoodieContext context)
      {
         _context = context;

      }

      [HttpGet(Name = "GetFavorites")]
      public async Task<ActionResult<Favorites>> GetFavorites(string userId)
      {
         Favorites favorites = await RetrieveFavorites(userId);

         if (favorites == null) {
            //Todo implement getting cookie data
            return NotFound();
         };

         return Ok(favorites);
      }

      [HttpPost("AddRecipe")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([FromQuery] string userId, int recipeId)
      {
         var favorites = await InitializeFavorites(userId);

         var recipe = await _context.Recipes.FindAsync(recipeId);
         if (recipe == null) return NotFound();

         favorites.AddFavoriteRecipe(recipe);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
      }

      [HttpPost("AddRestaurant")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRestaurant([FromQuery] string userId, int restaurantId)
      {
         var favorites = await InitializeFavorites(userId);

         var restaurant = await _context.Restaurants.FindAsync(restaurantId);
         if (restaurant == null) return NotFound();

         favorites.AddFavoriteRestaurant(restaurant);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
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
