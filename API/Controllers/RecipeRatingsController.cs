using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   public class RecipeRatingsController: BaseApiController
    {
        private readonly FoodieContext _context;
        public RecipeRatingsController(FoodieContext context)
        {
         _context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<RecipeRatingsDto>> GetRecipeRatings()
        {
            var recipes = await _context.Recipes.ToListAsync();
            var recipeRatings = await _context.RecipeRatings.ToListAsync();

            var recipeRatingsDto = recipeRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipes.Find(el => el.Id == rec.RecipeId).Name,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });

            return Ok(recipeRatingsDto);
        }
    }
}