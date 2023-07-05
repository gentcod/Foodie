using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BookmarksController: BaseApiController
    {
        private readonly FoodieContext _context;
        public BookmarksController(FoodieContext context)
        {
         _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<Bookmarks>> GetBookMarks()
        {
            var bookmarks = await _context.Bookmarks.ToListAsync();
            return Ok(bookmarks);
        }
    }
}