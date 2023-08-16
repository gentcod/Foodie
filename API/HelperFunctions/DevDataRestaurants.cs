using System.Text.Json;
using API.Models;

namespace API.HelperFunctions
{
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
            ImgSrc = rec.ImgSrc,
            Geolocation = new Bearing
            {
                Latitude = rec.Geolocation.Latitude,
                Longitude = rec.Geolocation.Longitude,
            },
         }).ToList();
      }
    }
}