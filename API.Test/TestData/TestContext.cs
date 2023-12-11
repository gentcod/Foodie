using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Test.TestData
{
    public class TestFoodieContext : FoodieContext
    {
        public TestFoodieContext(DbContextOptions<TestFoodieContext> options) : base(options)
        {}

        public virtual DbSet<Recipe> TestRecipes { get; set; }
        public virtual DbSet<Restaurant> TestRestaurants { get; set; }
    }

    public static class TestDBInitializer
    {
        public static void Intialize(TestFoodieContext testFoodieContext)
        {
            var testData = new TestContextData();

            var testRecipes = testData.TestRecipes;
            var testRestaurants = testData.TestRestaurants;

            testFoodieContext.TestRecipes.AddRange(testRecipes);
            testFoodieContext.TestRestaurants.AddRange(testRestaurants);
            testFoodieContext.SaveChanges();
        }
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

            TestDBInitializer.Intialize(dbContext);

            return dbContext;
        }
    }
}
