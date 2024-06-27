using API.Data;
using API.DTOs;
using API.Extensions;
using API.Models;
using API.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace API.Controllers;
public class CookieBookmarksController : BaseApiController
{
   private readonly FoodieContext _context;

   public CookieBookmarksController(FoodieContext context)
   {
      _context = context;
   }

   [HttpGet(Name = "GetCookieBookmark")]
   public ActionResult GetCookiesBookMarks()
   {
      Bookmarks bookmarks = RetrieveCookiesBookmarks(GetUserId());

      if (bookmarks == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "No bookmarks found"
        ));

        var data = bookmarks.MapBookmarksToDto();

        var response = ApiSuccessResponse<BookmarksDto>.Response(
            "success",
            "Bookmarks have been fetched successfully",
            data
        );

        return Ok(response);
   }

   [HttpPost("add")]
   public async Task<ActionResult> Add([BindRequired][FromQuery] BookmarkParams bookmarkParam)
   {
      var bookmarks = RetrieveCookiesBookmarks(GetUserId());
      bookmarks ??= CreateCookiesBookmarks(GetUserId());

      var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
      if (recipe == null) return NotFound();

      bookmarks.AddBookmark(recipe);
      var cookieBookmark = UpdateCookiesBookmark(bookmarks);
      if (cookieBookmark == null) return BadRequest(
         ApiErrorResponse.Response(
            "error",
            "Problem adding bookmark"
         )
      );
      
      var data = bookmarks.MapBookmarksToDto();

      return CreatedAtRoute("GetBookmark", data);
   }

   [HttpPost("remove")]
   public ActionResult Remove([BindRequired][FromQuery] BookmarkParams bookmarkParam)
   {
      Bookmarks bookmarks = RetrieveCookiesBookmarks(GetUserId());

      if (bookmarks == null) return NotFound(ApiErrorResponse.Response(
         "error",
         "No bookmarks found"
      ));

      bookmarks.RemoveBookmark(bookmarkParam.RecipeId);

      var cookieBookmark = UpdateCookiesBookmark(bookmarks);
      if (cookieBookmark == null) return BadRequest(
         ApiErrorResponse.Response(
            "error",
            "Problem removing bookmark"
         )
      );
      
      var data = bookmarks.MapBookmarksToDto();
      return Ok(
         ApiSuccessResponse<BookmarksDto>.Response(
            "success",
            "Bookmark has been added successfully",
            data
         )
      );
    }

   private string GetUserId()
   {
      return Request.Cookies["userId"];
   }

   private Bookmarks RetrieveCookiesBookmarks(string userId)
   {
      if (string.IsNullOrEmpty(userId))
      {
         Response.Cookies.Delete("userId");
         return null;
      }

      var bookmarkStr = Request.Cookies["bookmarks"];
      if (bookmarkStr == null) return null;

      var bookmark = JsonConvert.DeserializeObject<Bookmarks>(bookmarkStr);
      return bookmark;
   }

   private Bookmarks UpdateCookiesBookmark(Bookmarks bookmarks)
   {
      var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(7) };
      var bookmarkStr = JsonConvert.SerializeObject(bookmarks);
      Response.Cookies.Append("bookmarks", bookmarkStr, cookieOptions);
      return bookmarks;
   }

   private Bookmarks CreateCookiesBookmarks(string userId)
   {
      var bookmark = new Bookmarks { };
      var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(7) };

      _ = Guid.TryParse(userId, out var validId);
      if (string.IsNullOrEmpty(userId))
      {
         var userIdName = Guid.NewGuid().ToString();
         Response.Cookies.Append("userId", userIdName, cookieOptions);
      }

      bookmark.UserId = validId;
      var bookmarkStr = JsonConvert.SerializeObject(bookmark);
      Response.Cookies.Append("bookmarks", bookmarkStr, cookieOptions);
      return bookmark;

   }
}
