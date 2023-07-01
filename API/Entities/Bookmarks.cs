namespace API.Entities
{
   public class Bookmarks
    {
        public int Id { get; set; }
        public List<RecipeRef> Recipes { get; set; }

        public void AddBookmark(Recipe recipe)
        {
            if (Recipes.All(el => el.RecipeId != recipe.Id))
            {
                Recipes.Add(new RecipeRef {RecipeName = recipe.Name});
            }
        }

        public void RemoveBookmark(int recipeId)
        {
            var bookmark = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
            if(bookmark == null) return;

            Recipes.Remove(bookmark);
        }
    }
}