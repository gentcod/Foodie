using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
   public class RecipesContext : DbContext
    {
        public RecipesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}