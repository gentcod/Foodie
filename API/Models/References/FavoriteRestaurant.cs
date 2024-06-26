namespace API.Models.References;
public class FavoriteRestaurant
{
   public int Id { get; set; }

   public int RestaurantId { get; set; }
   public Restaurant Restaurant { get; set; }

   public int FavoritesId { get; set; }
   public Favorites Favorites { get; set; }
}