using API.DTOs;
using API.Models;
using API.Models.References;

namespace API.Extensions;
public static class BookmarksExtension
{
    public static IQueryable<BookmarksDto> MapBookmarksToDto(this IQueryable<Bookmarks> bookmarks)
    {
        return bookmarks.Select(book => new BookmarksDto
        {
            Id = book.Id,
            UserId = book.UserId,
            TotalBookmarks = book.TotalBookmarks,
            Recipes = book.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.RecipeId,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList(),
        });
    }
}