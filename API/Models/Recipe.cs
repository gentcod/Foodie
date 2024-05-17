namespace API.Models;
public class Recipe
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string Ingredients { get; set; }
   public required string Description { get; set; }
   public string ImageSrc { get; set; }
   public int CookTime { get; set; }
   public DateTime DateAdded { get; set; }
   public string Origin { get; set; }
   public double Rating { get; set; }
   public List<RecipeRating> RecipeRatings { get; set; }
   public required string Category { get; set; }
   public Boolean Featured { get; set; } = false;
}
