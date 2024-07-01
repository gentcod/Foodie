using API.Models.References;
using API.RequestHelpers;

namespace API.Models;
public class Favorites
{
    private const int maxFav = 10;
    public int Id { get; set; }
    public int TotalFavRecipes { get; set; }
    public int TotalFavRestaurants { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public List<FavoriteRecipe> Recipes { get; set; }
    public List<FavoriteRestaurant> Restaurants { get; set; }

    public ApiErrorResponse AddFavoriteRecipe(Recipe recipe)
    {
        if (TotalFavRecipes >= maxFav / 2) return ApiErrorResponse.Response(
            "error",
            "You have added maximum number of recipes to favorites. Remove an existing recipe then try again"
        );
        Recipes ??= [];

        Recipes.Add(new FavoriteRecipe
        {
            RecipeId = recipe.Id,
            Recipe = recipe,
            FavoritesId = Id,
        });

        TotalFavRecipes = Recipes.Count;
        return null;
    }

    public void RemoveFavoriteRecipe(int recipeId)
    {
        var favorite = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
        if (favorite == null) return;

        Recipes.Remove(favorite);
        TotalFavRecipes = Recipes.Count;
    }

    public ApiErrorResponse AddFavoriteRestaurant(Restaurant restaurant)
    {
        if (TotalFavRestaurants >= maxFav / 2) return ApiErrorResponse.Response(
            "error",
            "You have added maximum number of restaurants to favorites. Remove an existing restaurant then try again"
        );
        Restaurants ??= [];

        Restaurants.Add(new FavoriteRestaurant
        {
            RestaurantId = restaurant.Id,
            Restaurant = restaurant,
            FavoritesId = Id,
        });

        TotalFavRestaurants = Restaurants.Count;
        return null;
    }

    public void RemoveFavoriteRestaurant(int restaurantId)
    {
        var favorite = Restaurants.FirstOrDefault(el => el.RestaurantId == restaurantId);
        if (favorite == null) return;

        Restaurants.Remove(favorite);
        TotalFavRestaurants = Restaurants.Count;
    }
}