using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.Models;
using API.Test.TestData;
using Microsoft.AspNetCore.Mvc;

namespace API.Test;
public class TestRecipeRatingsController
{
   private readonly TestFoodieContext _testContext;
   private readonly TestContextData _testData;
   private readonly RecipeRatingsController _controller;
   public TestRecipeRatingsController()
   {
      _testContext = DbContextFactory.CreateInMemoryDbContext();
      _controller = new RecipeRatingsController(_testContext);
      _testData = new TestContextData();
   }

   [Fact]
   public async void GetRecipeRatings_Test()
   {
      var actionResult = await _controller.GetRecipeRatings();
      Assert.NotNull(actionResult);

      #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      var actionRes = (OkObjectResult)actionResult.Result;
      var result = actionRes!.Value;
      Assert.NotNull(result);

      //TODO: Resolve test for RecipeName
      // var recipes = _testData.TestRecipes;
      // var recipeRatingsTemp = _testData.TestRecipeRatings.MapRecipesRatingsToDto(recipes);
      // var recipeRatings = new List<RecipeRatingsDto>(recipeRatingsTemp);
      // Assert.Equal(result, recipeRatings[0].RecipeName);
   }
}
