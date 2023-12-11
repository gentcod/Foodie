using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.Test.TestData;
using Microsoft.AspNetCore.Mvc;

namespace API.Test
{
    public class TestRestaurantsController
    {
        [Fact]
        public async void GetRestaurants_Test()
        {
            var testContext = DbContextFactory.CreateInMemoryDbContext();

            var controller = new RestaurantController(testContext);
            var actionResult = await controller.GetRestaurants();
            Assert.NotNull(actionResult);

            var actionRes = (OkObjectResult) actionResult.Result;
            var result = actionRes.Value as List<RestaurantDto>;
            Assert.NotNull(result);

            var testData = new TestContextData();
            var restaurants = testData.TestRestaurants.MapRestaurantsToDto();
            Assert.Equal(result[0].Name, restaurants[0].Name);
        }
    }
}
