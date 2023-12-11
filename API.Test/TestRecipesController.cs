using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.RequestHelpers;
using API.Test.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Results;

namespace API.Test
{
    public class TestRecipesController
    {

        [Fact]
        public async Task GetRecipes_Test()
        {
            var testContext = DbContextFactory.CreateInMemoryDbContext();

            var controller = new RecipeController(testContext);

            var recipeParams = new RecipeParams { };
            var actionResult = await controller.GetRecipes(recipeParams);
            Assert.NotNull(actionResult);

            var actionRes = (OkObjectResult) actionResult.Result;
            var result = actionRes.Value as List<RecipeDto>;
            Assert.NotNull(result);
            
            var testData = new TestContextData();
            var recipes = testData.TestRecipes.MapRecipesToDto();
            Assert.Equal(result[0].Name, recipes[0].Name);
        }

        //TODO: Resolve GetRecipeById_Test
        // [Fact]
        // public async Task GetRecipesById_Test()
        // {
        //     var testContext = DbContextFactory.CreateInMemoryDbContext();

        //     var controller = new RecipeController(testContext);

        //     int recipeId = 1;
        //     var actionResult = await controller.GetRecipeById(recipeId);
        //     Assert.NotNull(actionResult);

        //     var actionRes = (OkObjectResult) actionResult.Result;
        //     var result = actionRes.Value as RecipeDto;
        //     Assert.NotNull(result);
            
        //     var testData = new TestContextData();
        //     var recipe = testData.TestRecipes.FirstOrDefault(rec => rec.Id == recipeId).MapRecipeToDto();
        //     Assert.Equal(result.Name, recipe.Name);
        // }
    }
}