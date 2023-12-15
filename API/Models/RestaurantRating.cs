namespace API.Models
{
   public class RestaurantRating
   {
      public int Id { get; set; }
      public int RestaurantId { get; set; }
      public Restaurant Restaurant { get; set; }
      public int RatingNum { get; set; }
      public string Comment { get; set; }
   }
}