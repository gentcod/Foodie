using API.Data;
using API.Models;
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
      public async Task<ActionResult<Bookmarks>> GetBookMark(string userId)
      {
         Bookmarks bookmarks = await RetrieveBookmarks(userId);

         if (bookmarks == null) return NotFound(new ProblemDetails{ Title= "Bookmarked Recipes could not be found"});
         return Ok(bookmarks);
      }

      [HttpPost("AddBookmark")]
      public async Task<ActionResult<Bookmarks>> AddNewBookMark([FromQuery] BookmarkParams bookmarkParam)
      {
         var bookmarks = await RetrieveBookmarks(bookmarkParam.UserId);
         if (bookmarks == null) bookmarks = CreateBookmarks(bookmarkParam.UserId);

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

      private Bookmarks CreateBookmarks(string userId)
      {
         //TODO: After resolving User Identity Roles, check if user idenity role exist
         // var userIdNameB = User.Identity?.Name;
         var bookmark = new Bookmarks{};

         if (userId == null)
         {
            var userIdName = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("userId", userIdName, cookieOptions);
            bookmark.UserId = userIdName;
            _context.Bookmarks.Add(bookmark);
            return bookmark;
         }

         bookmark.UserId = userId;
         _context.Bookmarks.Add(bookmark);

         return bookmark;
      }

      //TODO: Handle DB transaction to delete user cookie bookmarks after specific time from database
   }
}