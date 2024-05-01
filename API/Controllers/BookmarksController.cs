using API.Data;
using API.Models;
using API.RequestHelpers;
using API.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;

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

         if (bookmarks == null) return NotFound(new ProblemDetails { Title = "Bookmarked Recipes could not be found" });
         return Ok(bookmarks);
      }

      [HttpPost("AddBookmark")]
      public async Task<ActionResult<Bookmarks>> AddNewBookMark([BindRequired][FromQuery] BookmarkParams bookmarkParam)
      {
         var userId = GetUserId();
         var bookmarks = await RetrieveBookmarks(userId);
         bookmarks ??= CreateBookmarks(userId);

         var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
         if (recipe == null) return NotFound();

         bookmarks.AddBookmark(recipe);

         var result = await _context.SaveChangesAsync() > 0;
         if (result) return CreatedAtRoute("GetBookmark", bookmarks);

         return BadRequest(new ProblemDetails { Title = "Problem creating bookmark" });
      }

      private string GetUserId()
      {
         var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
         var name = userIdClaim.Value;
         return  name;
      }

      private async Task<Bookmarks> RetrieveBookmarks(string userId)
      {
         return await _context.Bookmarks
                 .Include(b => b.Recipes)
                 .ThenInclude(r => r.Recipe)
                 .FirstOrDefaultAsync(bookmark => bookmark.UserId == userId);
      }

      private Bookmarks CreateBookmarks(string userId)
      {
         var bookmark = new Bookmarks { };

         bookmark.UserId = userId;
         _context.Bookmarks.Add(bookmark);

         return bookmark;
      }

      //TODO: Handle DB transaction to delete user cookie bookmarks after specific time from database
   }
}