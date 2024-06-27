using System.Text.Json;
using API.Models;

namespace API.HelperFunctions;
/// <summary>
/// This class helps to read a stream of data from a json file with fields that match the Restaurant Object. It returns a list of Restaurant Objects
/// </summary>
public class DevDataRestaurants
{
   public List<Restaurant> RetrievedRestaurants { get; set; } = RetrieveDataFromJson();

   private static List<Restaurant> RetrieveDataFromJson()
   {
      var sepChar = Path.DirectorySeparatorChar;
      string path = $"dev-data{sepChar}restaurants.json";

      List<Restaurant> source = new List<Restaurant>();

      using (StreamReader r = new StreamReader(path))
      {
         string json = r.ReadToEnd();

         source = JsonSerializer.Deserialize<List<Restaurant>>(json);
      }

      return source.Select(rec => new Restaurant
      {
         Name = rec.Name,
         Location = rec.Location,
         ImageSrc = rec.ImageSrc,
         Geolocation = new Bearing
         {
            Latitude = rec.Geolocation.Latitude,
            Longitude = rec.Geolocation.Longitude,
         },
      }).ToList();
   }
}
