using System.Text.Json.Serialization;

namespace API.DTOs
{
   public class RecipeRatingsDto
    {
        public int RatingId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RecipeName { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}