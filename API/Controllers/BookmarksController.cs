using API.Data;
using API.Entities;
using API.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class BookmarksController : BaseApiController
   {
      private readonly FoodieContext _context;
      public BookmarksController(FoodieContext context)
      {
         _context = context;

      }

      [HttpGet(Name = "GetBookmark")]
      public async Task<ActionResult<Bookmarks>> GetBookMark([FromQuery] string userId)
      {
         Bookmarks bookmarks = await RetrieveBookmarks(userId);

         if (bookmarks == null) return NotFound();
         return Ok(bookmarks);
      }

      [HttpPost]
      public async Task<ActionResult<Bookmarks>> AddNewBookMark([FromQuery] BookmarkParams bookmarkParam)
      {
         var bookmarks = await RetrieveBookmarks(bookmarkParam.UserId);
         if (bookmarks == null) bookmarks = CreateBookmarks();

         var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
         if (recipe == null) return NotFound();

         bookmarks.AddBookmark(recipe);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetBookmark", bookmarks);

         return BadRequest(new ProblemDetails { Title = "Problem creating bookmark" });
      }

      private async Task<Bookmarks> RetrieveBookmarks(string userId)
      {
         if (string.IsNullOrEmpty(userId))
         {
            Response.Cookies.Delete("buyerId");
            return null;
         }
         return await _context.Bookmarks
                 .Include(b => b.Recipes)
                 .ThenInclude(r => r.Recipe)
                 .FirstOrDefaultAsync(rec => rec.UserId == userId);
      }

      private Bookmarks CreateBookmarks()
      {
         var userId = User.Identity?.Name;
         if (string.IsNullOrEmpty(userId))
         {
            userId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("userId", userId, cookieOptions);
         }

         var bookmark = new Bookmarks { UserId = userId };
         _context.Bookmarks.Add(bookmark);

         return bookmark;
      }
   }
}