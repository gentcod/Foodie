using API.DTOs;
using API.Entities;

namespace API.Extensions
{
   public static class RestaurantExtension
   {
      public static IEnumerable<RestaurantDto> MapBearingToRestaurant( this IQueryable<Restaurant> restaurants, IQueryable<Bearing> bearings)
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
               RatingRestaurant = restaurant.RatingRestaurant,
            }
         );

         return restaurantsDto;
      }
   }
}