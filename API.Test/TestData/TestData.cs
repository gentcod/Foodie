using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.TestData
{

    public class TestContextData
    {
        public List<Recipe> TestRecipes { get; set; } = GetTestRecipes();
        public List<Restaurant> TestRestaurants { get; set; } = GetTestRestaurants();

        private static List<Recipe> GetTestRecipes()
        {
            var testRecipes = new List<Recipe>
            {
                new Recipe
                {
                    Id = 1,
                    Name = "Food1",
                    Description = "Food1 Description",
                    Ingredients = "Food1 Ingredients",
                    CookTime = 30,
                    ImageSrc = "imgSrc1",
                    Origin = "Food1 Origin",
                    Rating = 5,
                    DateAdded = DateTime.Now,
                    RecipeRatings = new List<RatingRecipe>()
                },

                new Recipe
                {
                    Id = 2,
                    Name = "Food2",
                    Description = "Food2 Description",
                    Ingredients = "Food2 Ingredients",
                    CookTime = 35,
                    ImageSrc = "imgSrc2",
                    Origin = "Food2 Origin",
                    Rating = 4.5,
                    DateAdded = DateTime.Now,
                    RecipeRatings = new List<RatingRecipe>()
                }
            };

            return testRecipes;
        }

        private static List<Restaurant> GetTestRestaurants()
        {
            var testRestaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Restaurant1",
                    Location = "Location1",
                    ImgSrc = "imgSrc",
                    Rating = 5.0,
                    Geolocation = new Bearing(),
                    RestaurantRatings = new List<RatingRestaurant> { new RatingRestaurant { } },
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Restaurant2",
                    Location = "Location2",
                    ImgSrc = "imgSrc",
                    Rating = 5.0,
                    Geolocation = new Bearing(),
                    RestaurantRatings = new List<RatingRestaurant> { new RatingRestaurant { } },
                },
            };

            return testRestaurants;
        }
    }

}
