namespace API.DTOs
{
    public class ListedRecipeRatingsDto
   {
      public int RecipeId { get; set; }
      public string RecipeName { get; set; }
      public string ImageSrc { get; set; }
      public double RatingNum { get; set; }
      public int TotalRatings { get; set; }
   }
}