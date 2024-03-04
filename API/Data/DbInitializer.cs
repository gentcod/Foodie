using API.Models;
using API.HelperFunctions;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class DbInitializer
    {
       public static async Task Initialize(FoodieContext context, UserManager<User> userManager)
       {
            
            if (!userManager.Users.Any()) {
                var user = new User
                {
                    UserName = "bob",
                    Email = "bob@test.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
                
                var admin = new User
                {
                    UserName = "gentcodAdmin",
                    Email = "admin@gmail.com",
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, ["Member", "Admin"]);
            }

            if (context.Recipes.Any()) return;

            var retrievedDataRecipes = new DevDataRecipes();
            
            var recipes = retrievedDataRecipes.RetrievedRecipes;

            if (context.Restaurants.Any()) return;

            var retrievedDataRestaurants = new DevDataRestaurants();

            var restaurants = retrievedDataRestaurants.RetrievedRestaurants;

            context.Recipes.AddRange(recipes);
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
       }
    }

}