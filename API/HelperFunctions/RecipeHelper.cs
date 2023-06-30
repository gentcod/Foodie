using API.Entities;

namespace API.HelperFunctions
{
   public class RecipeHelper<T> : List<T>
    {
        public RecipeHelper(List<T> items)
        {
            RecipeList = GetRecipes();
        }

        public List<Recipe> Recipes { get; set; }
        public List<string> RecipeList { get; set; }

        public List<string> GetRecipes()
        {
            return Recipes.Select(recipe => recipe.Name).ToList();
        }
        
    }
}