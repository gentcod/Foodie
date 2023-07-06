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
                    Name = "Lounge 38",
                    Location = "67 Bode Thomas St, Surulere 101241, Lagos.",
                    Geolocation = new Bearing
                    {
                        Latitude = 6.4902,
                        Longitude = 3.3552,
                    }
                },
                new Restaurant
                {
                    Name = "Ofada Boy",
                    Location = "1 Mba St, Surulere, Lagos, Suru Lere, Lagos.",
                    Geolocation = new Bearing
                    {
                        Latitude = 6.4969,
                        Longitude = 3.3567,
                    }
                }
            };

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