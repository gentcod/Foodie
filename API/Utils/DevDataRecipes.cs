using System.Text.Json;
using API.Models;

namespace API.HelperFunctions
{
   /// <summary>
   /// This class helps to read a stream of data from a json file with fields that match the Recipe Object. It returns a list of Recipes
   /// </summary>
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
            DateAdded =  DateTime.Now.ToUniversalTime(),
            Origin = rec.Origin,
            Category = rec.Category,
         }).ToList();
      }
   }
}