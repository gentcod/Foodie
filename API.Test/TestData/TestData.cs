using API.Models;
using API.Models.References;

namespace API.Test.TestData
{
    /// <summary>
    /// This is a class that contains initialized data for tests
    /// </summary>
    public class TestContextData
    {
        public List<Recipe> TestRecipes { get; set; } = GetTestRecipes();
        public List<Restaurant> TestRestaurants { get; set; } = GetTestRestaurants();
        public List<Bookmarks> TestBookMarks { get; set; } = GetTestBookmarks();

        private static List<Recipe> GetTestRecipes()
        {
            var testRecipes = new List<Recipe>
            {
                new() {
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

                new() {
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
                new() {
                    Id = 1,
                    Name = "Restaurant1",
                    Location = "Location1",
                    ImgSrc = "imgSrc",
                    Rating = 5.0,
                    Geolocation = new Bearing(),
                    RestaurantRatings = new List<RatingRestaurant> { new() { } },
                },
                new() {
                    Id = 2,
                    Name = "Restaurant2",
                    Location = "Location2",
                    ImgSrc = "imgSrc",
                    Rating = 5.0,
                    Geolocation = new Bearing(),
                    RestaurantRatings = new List<RatingRestaurant> { new() { } },
                },
            };

            return testRestaurants;
        }
    
        private static List<Bookmarks> GetTestBookmarks()
        {
            var bookmarks = new List<Bookmarks> 
            {
                new() {
                    Id = 1,
                    UserId = "testUser1",
                },

                new() {
                    Id = 2,
                    UserId = "testUser2",
                },
            };

            return bookmarks;
        }
    }

}
