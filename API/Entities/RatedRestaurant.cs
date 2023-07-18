namespace API.Entities
{
   public class RatedRestaurant
    {
        public int Id { get; set; }
        public List<Restaurant> Restaurants { get; set; }  
    }
}