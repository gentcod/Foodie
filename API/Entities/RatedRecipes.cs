namespace API.Entities
{
   public class RatedRecipes
    {
        public int Id { get; set; }
        public List<RecipeRef> Recipes { get; set; }
        
    }
}