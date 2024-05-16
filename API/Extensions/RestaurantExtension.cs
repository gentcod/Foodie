using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class RestaurantExtension
{
   public static IEnumerable<RestaurantDto> MapBearingToRestaurant(this List<Bearing> bearings, List<Restaurant> restaurants)
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
         Rating = restaurant.Rating,
         Geolocation = bearing,
      }
      );

      return restaurantsDto;
   }

   public static List<RestaurantDto> MapRestaurantsToDto(this List<Restaurant> restaurants)
   {
      return restaurants.Select(res => new RestaurantDto
      {
         Id = res.Id,
         Name = res.Name,
         Location = res.Location,
         ImgSrc = res.ImgSrc,
         Rating = res.Rating,
         Geolocation = res.Geolocation,
      }).ToList();
   }

   public static RestaurantDto MapRestaurantToDto(this Restaurant restaurant)
   {
      return new RestaurantDto
      {
         Id = restaurant.Id,
         Name = restaurant.Name,
         Location = restaurant.Location,
         ImgSrc = restaurant.ImgSrc,
         Rating = restaurant.Rating,
         Geolocation = restaurant.Geolocation,
      };
   }
}