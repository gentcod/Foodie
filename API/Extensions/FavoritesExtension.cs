using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class FavoritesExtension
{
    public static IQueryable<FavoritesDto> MapFavoritesToDto(this IQueryable<Favorites> favorites)
    {
        return favorites.Select(fav => new FavoritesDto
        {
            Id = fav.Id,
            UserId = fav.UserId,
            TotalFavRecipes = fav.TotalFavRecipes,
            TotalFavRestaurants = fav.TotalFavRestaurants,
            Recipes = fav.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.Id,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList(),
            Restaurants = fav.Restaurants.Select(rec => new EmbeddedDto
            {
                Id = rec.Id,
                Name = rec.Restaurant.Name,
                ImageSrc = rec.Restaurant.ImgSrc,
            }).ToList(),
        });
    }
}