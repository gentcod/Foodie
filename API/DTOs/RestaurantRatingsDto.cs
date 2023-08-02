namespace API.DTOs
{
   public class RestaurantRatingsDto
    {
        public int RatingId { get; set; }
        public string RestaurantName { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}