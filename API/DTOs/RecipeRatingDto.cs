using System.Text.Json.Serialization;

namespace API.DTOs
{
   public class RecipeRatingDto
    {
        public int RatingId { get; set; }
        public string UserId { get; set; }
        public double RatingNum { get; set; }
        public string Comment { get; set; }
    }
}