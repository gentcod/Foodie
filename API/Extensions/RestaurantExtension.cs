using API.DTOs;
using API.Entities;

namespace API.Extensions
{
   public static class RestaurantExtension
   {
      private static Restaurant RetrieveRestaurant(this Bearing bearing, List<Restaurant> restaurants)
      {
         return restaurants.FirstOrDefault(a => a.Id == bearing.Id);
      }
   }
}