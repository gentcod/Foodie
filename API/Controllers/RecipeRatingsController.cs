using API.Data;
using API.DTOs;
using API.Models;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using API.RequestHelpers;
using System.Security.Claims;

namespace API.Controllers;
public class RecipeRatingsController : BaseApiController
{
    private readonly FoodieContext _context;
    public RecipeRatingsController(FoodieContext context)
    {
        _context = context;

    }

    [HttpGet(Name = "recipeRating")]
    public async Task<ActionResult> GetRecipeRatings([FromQuery] PaginationParams paginationParams)
    {
        var query = _context.RecipeRatings.Include(r => r.Recipe).AsQueryable();
        var recipesRatingsDto = query.MapRecipesRatingsToDto();

        var paginatedResponse = await PagedList<ListedRecipeRatingsDto>.ToPagedList(
            recipesRatingsDto, 
            paginationParams.PageNumber, 
            paginationParams.PageSize
        );

        Response.AddPaginationHeader(paginatedResponse.MetaData);
        
        return Ok(ApiSuccessResponse<PagedList<ListedRecipeRatingsDto>>.Response(
            "success",
            "Recipes ratings fetched successfully",
            paginatedResponse
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

        var recipeRatings = await _context.RecipeRatings
            .Include(rec => rec.Ratings)
            .FirstOrDefaultAsync(el => el.RecipeId == recipeId);
        if (recipeRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe rating not found"
        ));

        var recipeRatingsDto = recipeRatings.MapRecipeRatingsToDto(recipe);

        return Ok(ApiSuccessResponse<RecipeRatingsDto>.Response(
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

        var recipeRatings = await _context.RecipeRatings
            .FirstOrDefaultAsync(el => el.RecipeId == recipeId)
    ;
        recipeRatings ??= new RecipeRatings();

        recipeRatings.AddRating(new Rating
        {
            UserId = GetUserId(),
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        recipe.RecipeRatings = recipeRatings;
        recipe.RatingNum = recipeRatings.Ratings.Average(rating => rating.RatingNum);

        _context.Recipes.Update(recipe);
        var result = _context.SaveChangesAsync();
        var response = ApiSuccessResponse<RecipeRatingsDto>.Response(
            "success",
            "Recipe rating has been added successfully",
            recipeRatings.MapRecipeRatingsToDto(recipe!)
        );
        if (result != null) return CreatedAtRoute("recipeRating", response);

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding recipe rating"
        ));
    }

    [Authorize]
    [HttpPost("remove")]
    public async Task<ActionResult> Remove([BindRequired][FromQuery] int recipeId)
    {
        var recipe = await _context.Recipes.FirstOrDefaultAsync(el => el.Id == recipeId);
        if (recipe == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "Recipe not found"
        ));

        var recipeRatings = await _context.RecipeRatings.FirstOrDefaultAsync(el => el.RecipeId == recipe.Id);
        if (recipeRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe rating not found"
        ));

        var userId = GetUserId();
        var userRating = recipeRatings.Ratings.FirstOrDefault(rating => rating.UserId == userId);
        if (userRating == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User rating not found"
        ));

        recipeRatings.RemoveRating(userId);
        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            return Ok(
                ApiSuccessResponse<RecipeRatingsDto>.Response(
                    "success",
                    "Recipe rating has been removed successfully",
                    recipeRatings.MapRecipeRatingsToDto(recipe!)
                )
            );
        }

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem removing recipe rating"
        ));
    }

    private string GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        return userIdClaim.Value;
    }

}
