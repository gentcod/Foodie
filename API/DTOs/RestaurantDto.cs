using API.Models;

namespace API.DTOs
{
   public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageSrc { get; set; }
        public double RatingNum { get; set; }
        public Bearing Geolocation { get; set; }
    }
}