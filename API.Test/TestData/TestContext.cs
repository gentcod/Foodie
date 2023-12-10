using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.TestData
{
    public class TestFoodieContext : FoodieContext
    {
        public TestFoodieContext(DbContextOptions<TestFoodieContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Recipe> TestRecipes { get; set; }
        public virtual DbSet<Restaurant> TestRestaurant { get; set; }
    }



    public static class TestInitializer
    {
        public static void IntializeTest(TestFoodieContext testFoodieContext)
        {
            if (testFoodieContext.TestRecipes.Any()) return;
            var testData = new TestData();

            var testRecipes = testData.TestRecipes;

            testFoodieContext.TestRecipes.AddRange(testRecipes);
        }
    }


    public class TestData
    {
        public List<Recipe> TestRecipes { get; set; } = GetTestRecipes();

        private static List<Recipe> GetTestRecipes()
        {
            var testRecipes = new List<Recipe>();

            testRecipes.Add(new Recipe
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
            });

            testRecipes.Add(new Recipe
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
            });

            return testRecipes;
        }
    }
}
