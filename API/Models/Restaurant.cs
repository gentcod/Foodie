namespace API.Models
{
   public class Restaurant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string ImgSrc { get; set; }
        public double Rating { get; set; }
        public Bearing Geolocation { get; set; }
        public List<RatingRestaurant> RestaurantRatings { get; set; }
    }
}