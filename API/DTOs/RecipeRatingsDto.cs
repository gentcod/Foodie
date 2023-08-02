namespace API.DTOs
{
   public class RecipeRatingsDto
    {
        public int RatingId { get; set; }
        public string RecipeName { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}