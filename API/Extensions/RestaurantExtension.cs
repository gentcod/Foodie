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

      public static IEnumerable<RestaurantDto> MapRestaurantsToDto(this List<Restaurant> restaurants)
      {
         return restaurants.Select(res => new RestaurantDto
         {
            Id = res.Id,
            Name = res.Name,
            Location = res.Location,
            ImgSrc = res.ImgSrc,
            Geolocation = res.Geolocation,
            RestaurantRatings = res.RestaurantRatings,
         });
      }
   }
}