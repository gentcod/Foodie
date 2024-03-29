﻿using System.Collections.Specialized;
using System.Net;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Test.TestData;
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
    public virtual DbSet<Favorites> TestFavorites { get; set;}
    public virtual DbSet<RecipeRating> TestRecipeRatings { get; set;}
    public virtual DbSet<RestaurantRating> TestRestaurantRatings { get; set;}
}

/// <summary>
/// This adds test data to DbSet in DbContext
/// </summary>
public static class TestDBInitializer
{
    public static void Intialize(TestFoodieContext testFoodieContext)
    {
        var testData = new TestContextData();

        var testRecipes = testData.TestRecipes;
        var testRestaurants = testData.TestRestaurants;
        var testBookmarks = testData.TestBookMarks;
        var testFavorites = testData.TestFavorites;
        var testRecipeRatings = testData.TestRecipeRatings;
        var testRestaurantRatings = testData.TestRestaurantRatings;

        testFoodieContext.TestRecipes.AddRange(testRecipes);
        testFoodieContext.TestRestaurants.AddRange(testRestaurants);
        testFoodieContext.TestBookmarks.AddRange(testBookmarks);
        testFoodieContext.TestFavorites.AddRange(testFavorites);
        testFoodieContext.TestRecipeRatings.AddRange(testRecipeRatings);
        testFoodieContext.TestRestaurantRatings.AddRange(testRestaurantRatings);
        testFoodieContext.SaveChanges();
    }
}

public class TestHttpResponseHandler
{
    public HttpStatusCode StatusCode { get; set; }
    public NameValueCollection Headers { get; } = new NameValueCollection();
    public string Content { get; set; }

    public void Handle(HttpWebResponse response)
    {
        StatusCode = response.StatusCode;
        Headers.Clear();
        Headers.Add(response.Headers);
        
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            Content = reader.ReadToEnd();
        }
    }
}

/// <summary>
/// This initiliazes the in-memory DB connection and it initializes the DB context data.
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

        var handler = new TestHttpResponseHandler();

        return dbContext;
    }
}
