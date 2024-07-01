using API.Models.References;
using API.RequestHelpers;

namespace API.Models;
public class Bookmarks
{
    private const int maxBookmarks = 10;
    public int Id { get; set; }
    public int TotalBookmarks { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<BookmarkItem> Recipes { get; set; }

    public ApiErrorResponse AddBookmark(Recipe recipe)
    {
        if (TotalBookmarks >= maxBookmarks) return ApiErrorResponse.Response(
            "error",
            "You have added maximum number of bookmarks. Remove an existing bookmark then try again"
        );
        Recipes ??= [];

        Recipes.Add(new BookmarkItem
        {
            RecipeId = recipe.Id,
            Recipe = recipe,
            BookmarksId = Id,
        });

        TotalBookmarks = Recipes.Count;
        return null;
    }

    public void RemoveBookmark(int recipeId)
    {
        var bookmark = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
        if (bookmark == null) return;

        Recipes.Remove(bookmark);
        TotalBookmarks = Recipes.Count;
    }
}
