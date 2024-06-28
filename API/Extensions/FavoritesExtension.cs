using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class FavoritesExtension
{
    public static FavoritesDto MapFavoritesToDto(this Favorites favorites)
    {
        return new FavoritesDto
        {
            Id = favorites.Id,
            UserId = favorites.UserId.ToString(),
            TotalFavRecipes = favorites.TotalFavRecipes,
            TotalFavRestaurants = favorites.TotalFavRestaurants,
            Recipes = favorites.Recipes != null ? 
            favorites.Recipes.Select(rec => new EmbeddedDto
            {
                Id = rec.RecipeId,
                Name = rec.Recipe.Name,
                ImageSrc = rec.Recipe.ImageSrc,
            }).ToList()
            : new List<EmbeddedDto>(),
            Restaurants = favorites.Restaurants != null ?
            favorites.Restaurants.Select(rec => new EmbeddedDto
            {
                Id = rec.RestaurantId,
                Name = rec.Restaurant.Name,
                ImageSrc = rec.Restaurant.ImageSrc,
            }).ToList()
            : new List<EmbeddedDto>(),
        };
    }
}