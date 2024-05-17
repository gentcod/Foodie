// using API.Controllers;
// using API.Models;
// using API.RequestHelpers;
// using API.Test.TestData;
// using Microsoft.AspNetCore.Mvc;

// namespace API.Test;
// public class TestBookmarksController
// {
//    private readonly TestFoodieContext _testContext;
//    private readonly BookmarksController _controller;
//    private readonly TestContextData _testData;

//    public TestBookmarksController()
//    {
//       _testContext = DbContextFactory.CreateInMemoryDbContext();
//       _controller = new BookmarksController(_testContext);
//       _testData = new TestContextData();
//    }

//    [Fact]
//    public async void GetBookMarks_Test()
//    {
//       var userId = "testUser1";
//       var actionResult = await _controller.GetBookMark();
//       Assert.NotNull(actionResult);
      
//       #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
//       var actionRes = (OkObjectResult) actionResult.Result;
//       var result = actionRes!.Value as Bookmarks;
//       Assert.NotNull(result);

//       var bookmark = _testData.TestBookMarks.FirstOrDefault(bkm => bkm.UserId == userId);
//       Assert.Equal(result.Id, bookmark!.Id);
//    }

//    [Fact]
//    public async void AddNewBookMark_Test()
//    {
//       var bookmarkParams = new BookmarkParams
//       {
//          RecipeId = 1
//       };

//       var actionResult = await _controller.AddNewBookMark(bookmarkParams);
//       Assert.NotNull(actionResult);

//       var actionRes = (CreatedAtRouteResult) actionResult.Result;
//       var result = actionRes!.Value as Bookmarks;
//       Assert.NotNull(result);

//       var recipe = result.Recipes.FirstOrDefault(rec => rec.Id == bookmarkParams.RecipeId)!.Recipe;
//       var testRecipe = _testData.TestRecipes.FirstOrDefault(res => res.Id == bookmarkParams.RecipeId);
//       Assert.Equal(recipe.Name, testRecipe!.Name);
//    }
// }
