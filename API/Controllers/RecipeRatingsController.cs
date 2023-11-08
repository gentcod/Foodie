using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
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
            var recipesRatings = await _context.RecipeRatings.ToListAsync();

            var recipesRatingsDto = recipesRatings.MapRecipesRatingsToDto(recipes);

            return Ok(recipesRatingsDto);
        }

        [HttpGet(Name = "agg")]
        public async Task<ActionResult<RecipeRatingsDto>> GetRecipesAgg()
        {
            var recipes = await _context.Recipes.ToListAsync();
            var recipesRatings = await _context.RecipeRatings.ToListAsync();

            var recipesRatingsDto = recipesRatings.MapRecipesRatingsAggregatorToDto(recipes);

            return Ok(recipesRatingsDto);
        }

        [HttpGet("{recipeId}")]
        public async Task<ActionResult<RecipeRatingsDto>> GetRecipeRatingById(int recipeId)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(el => el.Id == recipeId);
            if (recipe == null) return BadRequest(new ProblemDetails{ Title = "Recipe not found"});

            var recipeRatings = await _context.RecipeRatings.Where(el => el.RecipeId == recipeId).ToListAsync();

            var recipeRatingDto = recipeRatings.MapRecipeRatingsToDto(recipe);

            return Ok(recipeRatingDto);
        }

        [HttpPost("AddRating/{recipeId}")]
        public async Task<ActionResult<RecipeRatingsDto>> AddRating(RatingDto ratingDto, int recipeId)
        {
            if (ratingDto.RatingNum < 1 || ratingDto.RatingNum > 5) return BadRequest(new ProblemDetails { Title = "Rating number is out of rating" });

            var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == recipeId);
            if (recipe == null) return BadRequest(new ProblemDetails { Title = "Recipe not found" });

            if (recipe.RecipeRatings == null) recipe.RecipeRatings = new List<RatingRecipe>();
            
            recipe.RecipeRatings.Add(new RatingRecipe
            {
                RatingNum = ratingDto.RatingNum,
                Comment = ratingDto.Comment,
            });

            var recipeRatings = await _context.RecipeRatings.Where(rec => rec.Id == recipeId).ToListAsync();
            var average = recipeRatings.Average(rating => rating.RatingNum);

            recipe.Rating = average;

            _context.Recipes.Update(recipe);
            var result = _context.SaveChangesAsync();
            if (result != null) return CreatedAtRoute("agg", ratingDto);

            return BadRequest(new ProblemDetails { Title = "Problem adding Recipe Rating" });
        }
    }
}