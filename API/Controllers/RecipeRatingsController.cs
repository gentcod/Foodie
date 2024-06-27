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
public class RecipeRatingsController(FoodieContext context) : BaseApiController
{
    private readonly FoodieContext _context = context;

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
    public async Task<ActionResult> AddRating([BindRequired] RatingDto ratingDto, [BindRequired][FromQuery] int recipeId)
    {
        _ = Guid.TryParse(GetUserId(), out var userId);
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == userId);
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User account not found"
        ));

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
            .Include(el => el.Ratings)
            .FirstOrDefaultAsync(el => el.RecipeId == recipeId);

        if (recipeRatings != null && recipeRatings.Ratings != null)
        {
            var existingRating = recipeRatings.Ratings.FirstOrDefault(el => el.UserId == userId);
            if (existingRating != null && recipeRatings.RecipeId == recipeId) return BadRequest(ApiErrorResponse.Response(
                    "error",
                    "Recipe rating has been previously added"
                ));
        }

        recipeRatings ??= new RecipeRatings();
        recipeRatings.AddRating(new Rating
        {
            User = user,
            RatingNum = ratingDto.RatingNum,
            Comment = ratingDto.Comment,
        });

        recipe.RecipeRatings = recipeRatings;
        var newRatingNum = recipeRatings.Ratings.Average(rating => rating.RatingNum);
        recipe.RatingNum = newRatingNum;

        var result = await _context.SaveChangesAsync() > 0;
        if (result)
        {
            var response = ApiSuccessResponse<RecipeRatingsDto>.Response(
                "success",
                "Recipe rating has been added successfully",
                recipeRatings.MapRecipeRatingsToDto(recipe!)
            );
            return CreatedAtRoute("recipeRating", response);
        }

        return BadRequest(ApiErrorResponse.Response(
            "error",
            "Problem adding recipe rating"
        ));
    }

    [Authorize]
    [HttpPost("remove")]
    public async Task<ActionResult> Remove([BindRequired][FromQuery] int recipeId)
    {
        _ = Guid.TryParse(GetUserId(), out var userId);
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == userId);
        if (user == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "User account not found"
        ));

        var recipe = await _context.Recipes.FirstOrDefaultAsync(el => el.Id == recipeId);
        if (recipe == null) return NotFound(ApiErrorResponse.Response(
            "error",
            "Recipe not found"
        ));

        var recipeRatings = await _context.RecipeRatings
            .Include(el => el.Ratings)
            .FirstOrDefaultAsync(el => el.RecipeId == recipe.Id);
        if (recipeRatings == null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Recipe rating not found"
        ));

        if (recipeRatings.Ratings != null)
        {
            var existingRating = recipeRatings.Ratings.FirstOrDefault(el => el.UserId == userId);
            if (existingRating == null) return NotFound(ApiErrorResponse.Response(
                    "error",
                    "User rating not found"
                ));
        }

        recipeRatings.RemoveRating(userId);

        double newRatingNum = recipeRatings.Ratings.Any() ? 
            recipeRatings.Ratings.Average(rating => rating.RatingNum)
            :0;
        recipe.RatingNum = newRatingNum;
        recipe.RecipeRatings = recipeRatings;

        var removedRating = await _context.Ratings.FirstOrDefaultAsync(el => el.UserId == userId);

        _context.Recipes.Update(recipe);
        _context.Ratings.Remove(removedRating);
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
