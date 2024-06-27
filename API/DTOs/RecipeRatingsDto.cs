namespace API.DTOs;
public class RecipeRatingsDto
{
   public int RecipeId { get; set; }
   public string RecipeName { get; set; }
   public string ImageSrc { get; set; }
   public double RatingNum { get; set; }
   public int TotalRatings { get; set; }

   public List<RecipeRatingDto> Ratings { get; set; }
}
