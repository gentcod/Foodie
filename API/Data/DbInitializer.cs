using API.Models;
using API.HelperFunctions;

namespace API.Data
{
    public static class DbInitializer
    {
       public static void Initialize(FoodieContext context)
       {
            if (context.Recipes.Any()) return;

            var retrievedDataRecipes = new DevDataRecipes();
            
            var recipes = retrievedDataRecipes.RetrievedRecipes;

            if (context.Restaurants.Any()) return;

            var retrievedDataRestaurants = new DevDataRestaurants();

            var restaurants = retrievedDataRestaurants.RetrievedRestaurants;

            if (context.Users.Any()) return;

            var users = new List<User>
            {
                new User
                {
                    Name = "Oyefule",
                    Password = "Pa$$w0rd",
                    Email = "oyefule@gmail.com",
                }
            };

            context.Recipes.AddRange(recipes);
            context.Restaurants.AddRange(restaurants);
            context.Users.AddRange(users);
            context.SaveChanges();
       }
    }

}