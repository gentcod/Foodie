using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      public async Task<ActionResult<Bookmarks>> GetBookMark([FromQuery] BookmarkParams bookmarkParam)
      {
         var bookmarkList = await _context.Bookmarks.ToListAsync();
         var bookmarks = bookmarkList.Where(bookmark => bookmark.UserId == bookmarkParam.UserId);
         return Ok(bookmarks);
      }

      [HttpPost]
      public async Task<ActionResult<Bookmarks>> AddNewBookMark([FromQuery] BookmarkParams bookmarkParam)
      {
        var recipes = await _context.Recipes.ToListAsync();

         var bookmarkList = await _context.Bookmarks.ToListAsync();
         var bookmarks = bookmarkList.Find(bookmark => bookmark.UserId == bookmarkParam.UserId);

         if (bookmarks == null) bookmarks = CreateBookmarks();

         bookmarks.AddBookmark(recipes, bookmarkParam.RecipeId);

         var result = _context.SaveChangesAsync();
         if (result != null) return CreatedAtRoute("GetBookmark", bookmarks);

         return BadRequest(new ProblemDetails { Title = "Problem creating bookmark" });
      }

      private Bookmarks CreateBookmarks()
      {
         var buyerId = User.Identity?.Name;
         if (string.IsNullOrEmpty(buyerId))
         {
            buyerId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
            Response.Cookies.Append("buyerId", buyerId, cookieOptions);
         }

         var bookmark = new Bookmarks { UserId = buyerId};
         _context.Bookmarks.Add(bookmark);

         return bookmark;
      }
   }
}