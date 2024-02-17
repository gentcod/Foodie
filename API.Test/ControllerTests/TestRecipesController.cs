using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.RequestHelpers;
using API.Test.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Test;

public class TestRecipesController
{
    private readonly TestFoodieContext _testContext;
    private readonly RecipeController _controller;
    private readonly TestContextData _testData;

    public TestRecipesController()
    {
        _testContext = DbContextFactory.CreateInMemoryDbContext();
        _controller = new RecipeController(_testContext);
        _testData = new TestContextData();
    }

    [Fact]
    public async Task GetRecipes_Test()
    {
        var recipeParams = new RecipeParams {
            PageNumber = 1,
            PageSize = 10
        };

        var actionResult = await _controller.GetRecipes(recipeParams);
        Assert.NotNull(actionResult);

        #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        var actionRes = (OkObjectResult) actionResult.Result;
        var result = actionRes!.Value as PagedList<RecipeDto>;
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        
        var recipesDto = _testData.TestRecipes.AsQueryable().MapRecipesToDto();
        var recipes = await PagedList<RecipeDto>.ToPagedList(recipesDto, recipeParams.PageNumber, recipeParams.PageSize);

        Assert.Equal(result[0].Name, recipes[0].Name);
        Assert.Equal(result[0].DateAdded, recipes[0].DateAdded);
    }

    [Fact]
    public async Task GetRecipesById_Test()
    {
        int recipeId = 1;
        var actionResult = await _controller.GetRecipeById(recipeId);
        Assert.NotNull(actionResult);

        var actionRes = (OkObjectResult) actionResult.Result;
        var result = actionRes!.Value as RecipeDto;
        Assert.NotNull(result);
        
        var recipe = _testData.TestRecipes.FirstOrDefault(rec => rec.Id == recipeId).MapRecipeToDto();
        Assert.Equal(result.Name, recipe.Name);
        Assert.Equal(result.DateAdded, recipe.DateAdded);
    }
}