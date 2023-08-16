using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class BearingController: BaseApiController
    {
        private readonly FoodieContext _context;
        public BearingController(FoodieContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<Bearing>> GetBearings()
        {
            var bearing = await _context.Bearing.ToListAsync();

            return Ok(bearing);
        }
    }
}