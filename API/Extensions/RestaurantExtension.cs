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
         ImgSrc = res.ImgSrc,
         Rating = res.Rating,
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
         ImgSrc = restaurant.ImgSrc,
         Rating = restaurant.Rating,
         Geolocation = restaurant.Geolocation,
      };
   }
}