namespace API.Models
{
   public class RecipeRating
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}