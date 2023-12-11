using API.Models.References;

namespace API.Models
{
   public class Bookmarks
    {
        public int Id { get; set; }
        public int TotalBookmarks { get; set; }

        public string UserId { get; set; }
        public List<BookmarkItem> Recipes { get; set; }

        public void AddBookmark(Recipe recipe)
        {
            if (Recipes == null) Recipes = new List<BookmarkItem>();

            var bookmark = Recipes.FirstOrDefault(el => el.RecipeId == recipe.Id);
            if (bookmark != null) return;
        
            Recipes.Add(new BookmarkItem 
            {
                RecipeId = recipe.Id, 
                Recipe = recipe,
                BookmarksId = Id,
            });

            TotalBookmarks = Recipes.Count();
        }

        public void RemoveBookmark(int recipeId)
        {
            var bookmark = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
            if(bookmark == null) return;

            Recipes.Remove(bookmark);
        }
    }
}