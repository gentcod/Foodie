using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   public class FoodieContext : IdentityDbContext<User, Role, int>
    {
        public FoodieContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Bookmarks> Bookmarks { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Bearing> Bearings { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RecipeRatings> RecipeRatings { get; set; }
        public DbSet<RestaurantRatings> RestaurantRatings { get; set; }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public DbSet<User> Users { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>();

            builder.Entity<Role>()
                .HasData(
                    new Role {Id = 1, Name = "Member", NormalizedName = "MEMBER" },
                    new Role {Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
                );
        }
    }
}