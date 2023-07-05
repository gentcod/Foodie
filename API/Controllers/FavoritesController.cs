using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class FavoritesController: BaseApiController
    {
        private readonly FoodieContext _context;
        public FavoritesController(FoodieContext context)
        {
         _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<Favorites>> GetFavorites()
        {
            var favorites = await _context.Favorites.ToListAsync();

            return Ok(favorites);
        }
    }
}