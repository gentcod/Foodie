namespace API.Entities
{
   public class Recipe
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public List<Ingredient> ingredients { get; set; }
   }
}