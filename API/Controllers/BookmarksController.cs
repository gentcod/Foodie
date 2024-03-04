using API.Data;
using API.Models;
using API.RequestHelpers;
using API.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class BookmarksController : BaseApiController
   {
      private readonly FoodieContext _context;
      private readonly JwtTokenGenerator _tokenGenerator;
      private readonly UserManager<User> _usermanager;


      public BookmarksController(FoodieContext context, JwtTokenGenerator tokenGenerator, UserManager<User> usermanager)
      {
         _context = context;
         _tokenGenerator = tokenGenerator;
         _usermanager = usermanager;
      }

      [HttpGet(Name = "GetBookmark")]
      public async Task<ActionResult<Bookmarks>> GetBookMark()
      {
         Bookmarks bookmarks = await RetrieveBookmarks(GetUserId());

         if (bookmarks == null) return NotFound(new ProblemDetails{ Title= "Bookmarked Recipes could not be found"});
         return Ok(bookmarks);
      }

      [HttpPost("AddBookmark")]
      public async Task<ActionResult<Bookmarks>> AddNewBookMark([BindRequired][FromQuery] BookmarkParams bookmarkParam)
      {
         var bookmarks = await RetrieveBookmarks(GetUserId());
         if (bookmarks == null) bookmarks = CreateBookmarks(GetUserId());

         var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
         if (recipe == null) return NotFound();

         bookmarks.AddBookmark(recipe);

         var user = await _usermanager.FindByNameAsync(GetUserId());
         if (user == null)
         {
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("bookmarks", JsonConvert.SerializeObject(bookmarks), cookieOptions);
            return CreatedAtAction("GetBookmark", bookmarks);
         }

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetBookmark", bookmarks);

         return BadRequest(new ProblemDetails { Title = "Problem creating bookmark" });
      }

      private string GetUserId()
      {
         return User.Identity?.Name ?? Request.Cookies["userId"];
      }

      private async Task<Bookmarks> RetrieveBookmarks(string userId)
      {
         if (string.IsNullOrEmpty(userId))
         {
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("bookmarks");
            return null;
         }
         

         return await _context.Bookmarks
                 .Include(b => b.Recipes)
                 .ThenInclude(r => r.Recipe)
                 .FirstOrDefaultAsync(bookmark => bookmark.UserId == userId);
      }

      private Bookmarks CreateBookmarks(string userId)
      {
         //TODO: After resolving User Identity Roles, check if user idenity role exist
         // var userIdNameB = User.Identity?.Name;
         var bookmark = new Bookmarks { };

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