namespace API.DTOs
{
    public class ListedRecipeDto
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string ImageSrc { get; set; }
      public string CookTime { get; set; }
      public string Origin { get; set; }
      public double RatingNum { get; set; }
      public string Category { get; set; }
      public Boolean Featured { get; set; }
   }
}