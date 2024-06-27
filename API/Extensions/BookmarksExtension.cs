using API.DTOs;
using API.Models;
using API.Models.References;

namespace API.Extensions;
public static class BookmarksExtension
{
    public static BookmarksDto MapBookmarksToDto(this Bookmarks bookmarks)
    {
        return  new BookmarksDto
        {
            UserId = bookmarks.UserId.ToString(),
            TotalBookmarks = bookmarks.TotalBookmarks,
            Recipes = bookmarks.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.RecipeId,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList(),
        };
    }
}