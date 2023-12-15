using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   public class FoodieContext : DbContext
    {
        public FoodieContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bookmarks> Bookmarks { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Bearing> Bearing { get; set; }
        public DbSet<RecipeRating> RecipeRatings { get; set; }
        public DbSet<RestaurantRating> RestaurantRatings { get; set; }
    }
}