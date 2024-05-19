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
            Recipes = fav.Recipes != null ? 
            fav.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.RecipeId,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList()
            : new List<EmbeddedDto>(),
            Restaurants = fav.Restaurants != null ?
            fav.Restaurants.Select(rec => new EmbeddedDto
            {
                Id = rec.RestaurantId,
                Name = rec.Restaurant.Name,
                ImageSrc = rec.Restaurant.ImgSrc,
            }).ToList()
            : new List<EmbeddedDto>(),
        });
    }
}