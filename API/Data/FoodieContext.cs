using API.Entities;
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
    }
}