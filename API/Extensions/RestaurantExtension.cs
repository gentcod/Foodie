using API.DTOs;
using API.Entities;

namespace API.Extensions
{
   public static class RestaurantExtension
   {
      public static IEnumerable<RestaurantDto> MapBearingToRestaurant( this List<Bearing> bearings, List<Restaurant> restaurants)
      {
         var restaurantsDto = bearings.Join(
         restaurants,
         bearing => bearing,
         restaurant => restaurant.Geolocation,
         (bearing, restaurant) => new RestaurantDto
         {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location,
            ImgSrc = restaurant.ImgSrc,
            Geolocation = bearing,
            RestaurantRatings = restaurant.RestaurantRatings,
         }
         );

         return restaurantsDto;
      }
   }
}