using API.Entities;

namespace API.DTOs
{
   public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImgSrc { get; set; }
        public Bearing Geolocation { get; set; }
        public List<RatingRestaurant> RestaurantRatings { get; set; }
    }
}