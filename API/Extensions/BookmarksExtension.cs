using API.DTOs;
using API.Models;
using API.Models.References;

namespace API.Extensions;
public static class BookmarksExtension
{
    public static IQueryable<BookmarksDto> MapBookmarksToDto(this IQueryable<Bookmarks> bookmarks)
    {
        return bookmarks.Select(fav => new BookmarksDto
        {
            Id = fav.Id,
            UserId = fav.UserId,
            TotalBookmarks = fav.TotalBookmarks,
            Recipes = fav.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.Id,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList(),
        });
    }
}