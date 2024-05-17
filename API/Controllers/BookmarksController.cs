using API.Data;
using API.Extensions;
using API.Models;
using API.RequestHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class BookmarksController : BaseApiController
{
    private readonly FoodieContext _context;

    public BookmarksController(FoodieContext context)
    {
        _context = context;
    }


    [HttpGet(Name = "GetBookmark")]
    public async Task<ActionResult<Bookmarks>> GetBookMark()
    {
        var userId = GetUserId();
        Bookmarks bookmarks = await RetrieveBookmarks(userId);

        if (bookmarks == null) return NotFound(new ProblemDetails
        {
            Detail = "Bookmarked Recipes could not be found"
        });

        IEnumerable<Bookmarks> enumerable = [bookmarks];
        var bookmarksResult = enumerable.AsQueryable();
        var respomse = bookmarksResult.MapBookmarksToDto();
        return Ok(respomse);
    }

    [HttpPost("AddBookmark")]
    public async Task<ActionResult<Bookmarks>> AddNewBookMark([BindRequired][FromQuery] BookmarkParams bookmarkParam)
    {
        var userId = GetUserId();
        var bookmarks = await RetrieveBookmarks(userId);
        bookmarks ??= CreateBookmarks(userId);

        var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
        if (recipe == null) return NotFound();

        if (bookmarks.Recipes != null)
        {
            var existingBookmark = bookmarks.Recipes.FirstOrDefault(rec => rec.RecipeId == recipe.Id);
            if (existingBookmark != null) return BadRequest(new ProblemDetails
            {
                Detail = "Recipe has been previously bookmarks"
            });
        }

        bookmarks.AddBookmark(recipe);

        IEnumerable<Bookmarks> enumerable = [bookmarks];
        var bookmarksResult = enumerable.AsQueryable();

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return CreatedAtRoute("GetBookmark", bookmarksResult.MapBookmarksToDto());

        return BadRequest(new ProblemDetails
        {
            Detail = "Problem creating bookmark"
        });
    }

    // private async Task<string> GetUserId()
    // {
    //     var emailClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
    //     var email = emailClaim.Value;
    //     var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    //     return user.UserId;
    // }

    private string GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        return userIdClaim.Value;
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
        var bookmark = new Bookmarks { UserId = userId };
        _context.Bookmarks.Add(bookmark);
        return bookmark;
    }
}