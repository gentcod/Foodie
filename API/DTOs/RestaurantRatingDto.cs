namespace API.DTOs
{
   public class RestaurantRatingDto
    {
        public int RatingId { get; set; }
        public string UserId { get; set; }
        public int RatingNum { get; set; }
        public string Comment { get; set; }
    }
}