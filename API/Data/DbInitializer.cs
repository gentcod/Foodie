using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   public static class DbInitializer
    {
       public static void Initialize(FoodieContext context)
       {
            if (context.Recipes.Any()) return;
            
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Jollof Rice",
                    Ingredients = "Pepper, Rice, Bulliion Cubes",
                    Description = "A very delicious meal",
                    Origin = "Yoruba"
                },
                new Recipe
                {
                    Name = "Fried Rice",
                    Ingredients = "Pepper, Rice, Bulliion Cubes, Curry",
                    Description = "A meal with delicate veggies",
                    Origin = "Yoruba"
                },
                new Recipe
                {
                    Name = "Efo Riro",
                    Ingredients = "Pepper, Spinnach, Bulliion Cubes, Palm Oil",
                    Description = "Goes well with swallows",
                    Origin = "Yoruba"
                }
            };

            if (context.Restaurants.Any()) return;

            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Asunde",
                    Location = "Akure",
                    Geolocation = new Bearing
                    {
                        Latitude = 7.287995329999998,
                        Longitude = 5.1475001939999998,
                    }
                }
            };

            context.Recipes.AddRange(recipes);
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
       }
    }

}