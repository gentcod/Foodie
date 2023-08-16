using API.Models.References;

namespace API.Models
{
   public class Favorites
    {
        public int Id { get; set; }
        public int TotalFavRecipes { get; set; }
        public int TotalFavRestaurants { get; set; }

        public string UserId { get; set; }
        public List<FavoriteRecipe> Recipes { get; set; }
        public List<FavoriteRestaurant> Restaurants { get; set; }

        public void AddFavoriteRecipe(Recipe recipe)
        {
            if (Recipes == null) Recipes = new List<FavoriteRecipe>();

            var favorite = Recipes.FirstOrDefault(el => el.RecipeId == recipe.Id);
            if (favorite != null) return;
        
            Recipes.Add(new FavoriteRecipe 
            {
                RecipeId = recipe.Id, 
                Recipe = recipe,
                FavoritesId = Id,
            });

            TotalFavRecipes = Recipes.Count();
        }

        public void RemoveFavoriteRecipe(int recipeId)
        {
            var favorite = Recipes.FirstOrDefault(el => el.RecipeId == recipeId);
            if(favorite == null) return;

            Recipes.Remove(favorite);
        }

        public void AddFavoriteRestaurant(Restaurant restaurant)
        {
            if (Restaurants == null) Restaurants = new List<FavoriteRestaurant>();

            var bookmark = Restaurants.FirstOrDefault(el => el.RestaurantId == restaurant.Id);
            if (bookmark != null) return;
        
            Restaurants.Add(new FavoriteRestaurant 
            {
                RestaurantId = restaurant.Id, 
                Restaurant = restaurant,
                FavoritesId = Id,
            });

            TotalFavRestaurants = Restaurants.Count();
        }

        public void RemoveFavoriteRestaurant(int restaurantId)
        {
            var favorite = Restaurants.FirstOrDefault(el => el.RestaurantId == restaurantId);
            if(favorite == null) return;

            Restaurants.Remove(favorite);
        }
    }
}