namespace API.Models
{
   public class RatingRecipe
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}