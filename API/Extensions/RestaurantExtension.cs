using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class RestaurantExtension
{
   public static IQueryable<RestaurantDto> MapRestaurantsToDto(this IQueryable<Restaurant> restaurants)
   {
      return restaurants.Select(res => new RestaurantDto
      {
         Id = res.Id,
         Name = res.Name,
         Location = res.Location,
         ImageSrc = res.ImageSrc,
         RatingNum = res.RatingNum,
         Geolocation = res.Geolocation,
      });
   }

   public static RestaurantDto MapRestaurantToDto(this Restaurant restaurant)
   {
      return new RestaurantDto
      {
         Id = restaurant.Id,
         Name = restaurant.Name,
         Location = restaurant.Location,
         ImageSrc = restaurant.ImageSrc,
         RatingNum = restaurant.RatingNum,
         Geolocation = restaurant.Geolocation,
      };
   }
}