namespace API.Models;
public class RestaurantRating : Rating
{
   public int RestaurantId { get; set; }
   public Restaurant Restaurant { get; set; }
}
