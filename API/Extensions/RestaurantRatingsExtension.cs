using API.Models;
using API.DTOs;

namespace API.Extensions;
public static class RestaurantRatingsExtension
{
    public static IQueryable<ListedRestaurantRatingsDto> MapRestaurantsRatingsToDto(this IQueryable<RestaurantRatings> restaurantsRatings)
    {
        return restaurantsRatings.Select(rec => new ListedRestaurantRatingsDto
        {
            RestaurantId = rec.RestaurantId,
            RestaurantName = rec.Restaurant.Name,
            ImageSrc = rec.Restaurant.ImageSrc,
            RatingNum = rec.Restaurant.RatingNum,
            TotalRatings = rec.TotalRatings
        });
    }

    public static RestaurantRatingsDto MapRestaurantRatingsToDto(this RestaurantRatings restaurantRatings, Restaurant restaurant)
    {
        return new RestaurantRatingsDto
        {
            RestaurantId = restaurantRatings.RestaurantId,
            RestaurantName = restaurant.Name,
            ImageSrc = restaurant.ImageSrc,
            RatingNum = restaurant.RatingNum,
            TotalRatings = restaurantRatings.TotalRatings,
            Ratings = restaurantRatings.Ratings != null ?
            restaurantRatings.Ratings.Select(rating => new RestaurantRatingDto
            {
                RatingId = rating.Id,
                UserId = rating.UserId,
                RatingNum = rating.RatingNum,
                Comment = rating.Comment,
            }).ToList()
            : [],
        };
    }
}