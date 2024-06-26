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
public class BookmarksController(FoodieContext context) : BaseApiController
{
    private readonly FoodieContext _context = context;

    [HttpGet(Name = "GetBookmark")]
    public async Task<ActionResult> GetBookMark()
    {
        Bookmarks bookmarks = await RetrieveBookmarks(GetUserId());

        if (bookmarks == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "No bookmarks found"
        ));
        
        var data = bookmarks.MapBookmarksToDto();

        return Ok(ApiSuccessResponse<BookmarksDto>.Response(
            "success",
            "Bookmarks have been fetched successfully",
            data
        ));
    }

    [HttpPost("add/{recipeId}")]
    public async Task<ActionResult> Add([BindRequired][FromRoute] int recipeId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User not found"
        ));

        var bookmarks = await RetrieveBookmarks(GetUserId());
        bookmarks ??= CreateBookmarks(GetUserId());


        var recipe = await _context.Recipes.FindAsync(recipeId);
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

        var errResp = bookmarks.AddBookmark(recipe);
        if (errResp != null) return BadRequest(errResp);

        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            var data = bookmarks.MapBookmarksToDto();

            var response = ApiSuccessResponse<BookmarksDto>.Response(
                "success",
                "Bookmark has been added successfully",
                data
            );
            return CreatedAtRoute("GetBookmark", response);
        }

        return BadRequest(
            ApiErrorResponse.Response(
                "error",
                "Problem adding bookmark"
            )
        );
    }

    [HttpDelete("remove/{recipeId}")]
    public async Task<ActionResult> Remove([BindRequired][FromRoute] int recipeId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User not found"
        ));

        Bookmarks bookmarks = await RetrieveBookmarks(GetUserId());

        if (bookmarks == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "No bookmarks found"
        ));

        var recipe = bookmarks.Recipes.FirstOrDefault(rec => rec.RecipeId == recipeId);
        if (recipe == null) return NotFound(
            ApiErrorResponse.Response(
                "error",
                "Recipe is not bookmarked"
            )
        );

        bookmarks.RemoveBookmark(recipeId);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            var data = bookmarks.MapBookmarksToDto();

            return Ok(
                ApiSuccessResponse<BookmarksDto>.Response(
                    "success",
                    "Bookmark has been removed successfully",
                    data
                )
            );
        }

        return BadRequest(
            ApiErrorResponse.Response(
                "error",
                "Problem removing bookmark"
            )
        );
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
         _ = Guid.TryParse(userIdClaim.Value, out var userId);
        return userId;
    }

    private async Task<Bookmarks> RetrieveBookmarks(Guid userId)
    {
        return await _context.Bookmarks
                .Include(b => b.Recipes)
                .ThenInclude(r => r.Recipe)
                .FirstOrDefaultAsync(bookmark => bookmark.UserId == userId);
    }

    private Bookmarks CreateBookmarks(Guid userId)
    {
        var bookmark = new Bookmarks { UserId = userId };
        _context.Bookmarks.Add(bookmark);
        return bookmark;
    }
}