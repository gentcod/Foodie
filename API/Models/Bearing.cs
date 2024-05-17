namespace API.Models;
public class Bearing
{
   public int Id { get; set; }

   public int RestaurantId { get; set; }
   public double Latitude { get; set; }
   public double Longitude { get; set; }
}
