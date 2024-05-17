using System.Net;
using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.Models;
using API.RequestHelpers;
using API.Test.TestData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Test;

public class TestRestaurantsController
{
    private readonly TestFoodieContext _testContext;
    private readonly RestaurantController _controller;
    private readonly TestContextData _testData;

    public TestRestaurantsController()
    {
        _testContext = DbContextFactory.CreateInMemoryDbContext();
        _controller = new RestaurantController(_testContext);
        _testData = new TestContextData();
    }

    // [Fact]
    // public async void GetRestaurants_Test()
    // {
    //     var context = new DefaultHttpContext();
    //     var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
    //     mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);
    //     var mockContext = mockHttpContextAccessor.Object.HttpContext;

    //     var paginationParams = new PaginationParams
    //     {
    //         PageNumber = 1,
    //         PageSize = 10,
    //     };
    //     var actionResult = await _controller.GetRestaurants(paginationParams);
    //     Assert.NotNull(actionResult);

    //     #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    //     var actionRes = (OkObjectResult) actionResult.Result;
    //     var result = actionRes!.Value as List<RestaurantDto>;
    //     Assert.NotNull(result);

    //     // var testData = new TestContextData();
    //     var restaurants = _testData.TestRestaurants;
    //     var enumerable = (IEnumerable<Restaurant>)restaurants;
    //     var restaurantData = enumerable.AsQueryable().MapRestaurantsToDto();
    //     var paginatedResponse = await PagedList<RestaurantDto>.ToPagedList(
    //         restaurantData, 
    //         paginationParams.PageNumber, 
    //         paginationParams.PageSize
    //     );
    //     var response = new HttpResponse(HttpStatusCode.OK, paginatedResponse);
    //     response.AddPaginationHeader(paginatedResponse.MetaData);
    //     Assert.Equal(result[0].Name, paginatedResponse[0].Name);
    // }

    [Fact]
    public async void GetRestaurantById_Test()
    {
        var restaurantId = 1;
        var actionResult = await _controller.GetRestaurantById(restaurantId);
        Assert.NotNull(actionResult);

        var actionRes = (OkObjectResult) actionResult.Result;
        var result = actionRes!.Value as RestaurantDto;
        Assert.NotNull(result);

        var restaurant = _testData.TestRestaurants.FirstOrDefault(res => res.Id == restaurantId).MapRestaurantToDto();
        Assert.Equal(result.Name, restaurant.Name);
    }
}

