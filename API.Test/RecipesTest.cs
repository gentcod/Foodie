using API.Controllers;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Models;
using API.RequestHelpers;
using API.Test.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace API.Test
{
    public class RecipesTest
    {

        [Fact]
        public async Task GetRecipes_Test()
        {
            var testContext = DbContextFactory.CreateInMemoryDbContext();

            var controller = new RecipeController(testContext);

            var recipeParams = new RecipeParams { };
            var actionResult = await controller.GetRecipes(recipeParams);
            Assert.NotNull(actionResult);

            RecipeDto? result = actionResult.Value;
            if (result == null)
            {
                return;
            }

            var recipes = testContext.Recipes.ToList().MapRecipesToDto();
            Assert.Equal((IEnumerable<RecipeDto>)result, recipes);
        }

        public class DbContextFactory
        {
            public static TestFoodieContext CreateInMemoryDbContext()
            {
                var optionsBuilder = new DbContextOptionsBuilder<TestFoodieContext>();
                optionsBuilder.UseSqlite("DataSource=:memory:", x => { });

                var dbContext = new TestFoodieContext(optionsBuilder.Options);
                dbContext.Database.OpenConnection();
                dbContext.Database.EnsureCreated();

                return dbContext;
            }
        }
    }
}