namespace API.DTOs;
public class ListedRestaurantRatingsDto
{
   public int RestaurantId { get; set; }
   public string RestaurantName { get; set; }
   public string ImageSrc { get; set; }
   public double RatingNum { get; set; }
   public int TotalRatings { get; set; }
}
