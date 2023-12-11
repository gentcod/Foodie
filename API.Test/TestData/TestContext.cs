﻿using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Test.TestData
{
    /// <summary>
    /// This is a class for the test data context.
    /// It inherits from the API.Data.FoodieContext which is needed for dependency injection to initialize the API controllers.
    /// </summary>
    public class TestFoodieContext : FoodieContext
    {
        public TestFoodieContext(DbContextOptions<TestFoodieContext> options) : base(options)
        {}

        public virtual DbSet<Recipe> TestRecipes { get; set; }
        public virtual DbSet<Restaurant> TestRestaurants { get; set; }
        public virtual DbSet<Bookmarks> TestBookmarks { get; set; }
    }

    public static class TestDBInitializer
    {
        public static void Intialize(TestFoodieContext testFoodieContext)
        {
            var testData = new TestContextData();

            var testRecipes = testData.TestRecipes;
            var testRestaurants = testData.TestRestaurants;
            var testBookmarks = testData.TestBookMarks;

            testFoodieContext.TestRecipes.AddRange(testRecipes);
            testFoodieContext.TestRestaurants.AddRange(testRestaurants);
            testFoodieContext.TestBookmarks.AddRange(testBookmarks);
            testFoodieContext.SaveChanges();
        }
    }

    /// <summary>
    /// This initiliazes the in-memory DB connection adds test data to DBSet in DbContext.
    /// </summary>
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