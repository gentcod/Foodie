namespace API.Entities
{
   public class RatedRecipe
    {
        public int Id { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public List<Recipe> RatedRecipes { get; set; }
        
    }

}