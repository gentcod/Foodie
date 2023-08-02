namespace API.Entities
{
   public class RatingRestaurant
   {
      public int Id { get; set; }
      public int RestaurantId { get; set; }
      public int RatingNum { get; set; }
      public string Comment { get; set; }
   }
}