using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class RecipeDto
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Ingredients { get; set; }
      public string Description { get; set; }
      public string ImageSrc { get; set; }
      public string CookTime { get; set; }
      public string DateAdded { get; set; }
      public string Origin { get; set; }
      public double Rating { get; set; }
   }
}