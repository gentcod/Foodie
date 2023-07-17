namespace API.Entities
{
   public class Bookmarks
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public List<RecipeRef> Recipes { get; set; }

        public void AddBookmark(List<Recipe> recipes, int recipeId)
        {
            var recipeBookmark = recipes.FirstOrDefault(el => el.Id == recipeId);
            Recipes = new List<RecipeRef>();
            Recipes.Add(new RecipeRef {RecipeName = recipeBookmark.Name});
        }

        public void RemoveBookmark(int recipeId)
        {
            var bookmark = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
            if(bookmark == null) return;

            Recipes.Remove(bookmark);
        }
    }
}