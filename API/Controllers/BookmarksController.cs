using API.Data;
using API.DTOs;
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
    public async Task<ActionResult> GetBookMark()
    {
        var userId = GetUserId();
        Bookmarks bookmarks = await RetrieveBookmarks(userId);

        if (bookmarks == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "No bookmarks found"
        ));

        IEnumerable<Bookmarks> enumerable = [bookmarks];
        var bookmarksResult = enumerable.AsQueryable();
        var data = bookmarksResult.MapBookmarksToDto();

        return Ok(ApiSuccessResponse<IQueryable<BookmarksDto>>.Response(
            "success",
            "Bookmarks have been fetched successfully",
            data
        ));
    }

    [HttpPost("AddBookmark")]
    public async Task<ActionResult> AddNewBookMark([BindRequired][FromQuery] BookmarkParams bookmarkParam)
    {
        var userId = GetUserId();
        var bookmarks = await RetrieveBookmarks(userId);
        bookmarks ??= CreateBookmarks(userId);

        var recipe = await _context.Recipes.FindAsync(bookmarkParam.RecipeId);
        if (recipe == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe not found"
        ));

        if (bookmarks.Recipes != null)
        {
            var existingBookmark = bookmarks.Recipes.FirstOrDefault(rec => rec.RecipeId == recipe.Id);
            if (existingBookmark != null) return BadRequest(ApiErrorResponse.Response(
                "error",
                "Recipe has been previously bookmarked"
            ));
        }

        bookmarks.AddBookmark(recipe);

        IEnumerable<Bookmarks> enumerable = [bookmarks];
        var bookmarksResult = enumerable.AsQueryable();
        var data = bookmarksResult.MapBookmarksToDto();

        var response = ApiSuccessResponse<IQueryable<BookmarksDto>>.Response(
            "success",
            "Bookmark has been added successfully",
            data
        );

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return CreatedAtRoute("GetBookmark", response);

        return BadRequest(
            ApiErrorResponse.Response(
                "error",
                "Problem adding bookmark"
            )
        );
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