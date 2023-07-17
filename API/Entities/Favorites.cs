namespace API.Entities
{
   public class Favorites
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public List<RecipeRef> Recipes { get; set; }

        public void AddFavorite(Recipe recipe)
        {
            if (Recipes.All(el => el.RecipeId != recipe.Id))
            {
                Recipes.Add(new RecipeRef {RecipeName = recipe.Name});
            }
        }

        public void RemoveFavorite(int recipeId)
        {
            var favorite = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
            if(favorite == null) return;

            Recipes.Remove(favorite);
        }
    }
}