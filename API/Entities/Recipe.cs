namespace API.Entities
{
   public class Recipe
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Ingredients { get; set; }
      public string Description { get; set; }
      public DateTime cookTime { get; set; }
      public string Origin { get; set; }
      public Rating RatingRecipe { get; set;}
   }
}

//name,, origin, description, ongredients, cookTime, ratingRecipe