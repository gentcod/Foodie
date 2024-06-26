using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using API.RequestHelpers;

namespace API.Controllers;
public class RecipeRatingsController : BaseApiController
{
    private readonly FoodieContext _context;
    public RecipeRatingsController(FoodieContext context)
    {
        _context = context;

    }

    [HttpGet(Name = "recipeRating")] //It returns all the individual recipe ratings
    public async Task<ActionResult> GetRecipeRatings()
    {
        var recipes = await _context.Recipes.ToListAsync();
        var recipesRatings = await _context.RecipeRatings.ToListAsync();

        var recipesRatingsDto = recipesRatings.MapRecipesRatingsToDto(recipes);

        return Ok(ApiSuccessResponse<IEnumerable<RecipeRatingsDto>>.Response(
            "success",
            "Recipes ratings fetched successfully",
            recipesRatingsDto
        ));
    }

    [HttpGet("agg", Name = "agg")] //It returns an aggregated recipe list w
    public async Task<ActionResult> GetRecipesAgg()
    {
        var recipes = await _context.Recipes.ToListAsync();
        var recipesRatings = await _context.RecipeRatings.ToListAsync();

        var recipesRatingsDto = recipesRatings.MapRecipesRatingsAggregatorToDto(recipes);

        return Ok(ApiSuccessResponse<IEnumerable<RecipeRatingsDto>>.Response(
            "success",
            "Aggregated recipes ratings fetched successfully",
            recipesRatingsDto
        ));
    }

    [HttpGet("{recipeId}")]
    public async Task<ActionResult> GetRecipeRatingById([BindRequired] int recipeId)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(el => el.Id == recipeId);
        if (recipe == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe not found"
        ));

        var recipeRatings = await _context.RecipeRatings.Where(el => el.RecipeId == recipeId).ToListAsync();

        var recipeRatingsDto = recipeRatings.MapRecipeRatingsToDto(recipe);

        return Ok(ApiSuccessResponse<IEnumerable<RecipeRatingsDto>>.Response(
            "success",
            "Recipe ratings fetched successfully",
            recipeRatingsDto
        ));
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<RecipeRatingsDto>> AddRating(RatingDto ratingDto, [BindRequired][FromQuery] int recipeId)
    {
        if (ratingDto.RatingNum < 1 || ratingDto.RatingNum > 5) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Rating number is out of range"
        ));

        var recipe = await _context.Recipes.FirstOrDefaultAsync(rec => rec.Id == recipeId);
        if (recipe == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe not found"
        ));

        var recipeRatings = await _context.RecipeRatings.Where(el => el.RecipeId == recipeId).ToListAsync();
        if (recipeRatings == null) recipeRatings ??= new List<RecipeRating>();

        recipeRatings.Add(new RecipeRating
        {
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        recipe.RecipeRatings = recipeRatings;
        recipe.Rating = Math.Round(recipeRatings.Average(rating => rating.RatingNum), 1); ;

        _context.Recipes.Update(recipe);
        var result = _context.SaveChangesAsync();
        var response = ApiSuccessResponse<IEnumerable<RecipeRatingsDto>>.Response(
            "success",
            "Recipe rating added successfully",
            recipeRatings.MapRecipeRatingsToDto(recipe)
        );
        if (result != null) return CreatedAtRoute("recipeRating", response);

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding Recipe Rating"
        ));
    }
}

// TODO: Return Ratings for Recipe and Restaurant as PagedList