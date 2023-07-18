using API.Data;
using API.Entities;
using API.RequestHelpers;
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
      public async Task<ActionResult<Favorites>> GetFavorites([FromQuery] string userId)
      {
         Favorites favorites = await RetrieveFavorites(userId);

         if (favorites == null) return NotFound();
         return Ok(favorites);
      }

      [HttpPost("Recipes")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRecipe([FromQuery] string userId, int recipeId)
      {
         var favorites = await RetrieveFavorites(userId);
         if (favorites == null) favorites = CreateFavorites();

         var recipe = await _context.Recipes.FindAsync(recipeId);
         if (recipe == null) return NotFound();

         favorites.AddFavoriteRecipe(recipe);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
      }

      [HttpPost("Restaurants")]
      public async Task<ActionResult<Favorites>> AddNewFavoriteRestaurant([FromQuery] string userId, int restaurantId)
      {
         var favorites = await RetrieveFavorites(userId);
         if (favorites == null) favorites = CreateFavorites();

         var restaurant = await _context.Restaurants.FindAsync(restaurantId);
         if (restaurant == null) return NotFound();

         favorites.AddFavoriteRestaurant(restaurant);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetFavorites", favorites);

         return BadRequest(new ProblemDetails { Title = "Problem adding Favorite" });
      }

      private async Task<Favorites> RetrieveFavorites(string userId)
      {
         if (string.IsNullOrEmpty(userId))
         {
            Response.Cookies.Delete("buyerId");
            return null;
         }

         var favorite = await _context.Favorites
                  .Include(b => b.Recipes)
                  .ThenInclude(r => r.Recipe)
                  .Include(f => f.Restaurants)
                  .ThenInclude(r => r.Restaurant)
                  .FirstOrDefaultAsync(rec => rec.UserId == userId);

        return favorite;
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

         var bookmark = new Favorites { UserId = userId };
         _context.Favorites.Add(bookmark);

         return bookmark;
      }
   }
}
