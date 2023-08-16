using API.Models;
using API.DTOs;

namespace API.Extensions
{
   public static class RestaurantRatingsExtension
    {
        public static IEnumerable<RestaurantRatingsDto> MapRestaurantsRatingsToDto(this List<RatingRestaurant> restaurantRatings, List<Restaurant> restaurants)
        {
            return restaurantRatings.Select(rec => new RestaurantRatingsDto
            {
                RatingId = rec.Id,
                RestaurantName = restaurants.Find(el => el.Id == rec.RestaurantId).Name,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });
        }

        public static IEnumerable<RestaurantRatingsDto> MapRestaurantRatingsToDto(this List<RatingRestaurant> restaurantRatings, Restaurant restaurant)
        {
            return restaurantRatings.Select(rec => new RestaurantRatingsDto
            {
                RatingId = rec.Id,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });
        }
    }
}