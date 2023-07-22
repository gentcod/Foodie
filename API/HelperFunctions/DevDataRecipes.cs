using System.Text.Json;
using API.Entities;

namespace API.HelperFunctions
{
   public class DevDataRecipes
   {
      public List<Recipe> RetrievedRecipes { get; set; } = RetrieveDataFromJson();

      private static List<Recipe> RetrieveDataFromJson()
      {
         var sepChar = Path.DirectorySeparatorChar;
         string path = $"dev-data{sepChar}recipes.json";

         List<Recipe> source = new List<Recipe>();

         using (StreamReader r = new StreamReader(path))
         {
            string json = r.ReadToEnd();

            source = JsonSerializer.Deserialize<List<Recipe>>(json);
         }

         return source.Select(rec => new Recipe
         {
            Name = rec.Name,
            Ingredients = rec.Ingredients,
            ImageSrc = rec.ImageSrc,
            Description = rec.Description,
            CookTime = rec.CookTime,
            DateAdded = new DateTime(),
            Origin = rec.Origin,
         }).ToList();
      }
   }
}