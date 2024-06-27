using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class RecipeRatingsExtension
{
    public static IQueryable<ListedRecipeRatingsDto> MapRecipesRatingsToDto(this IQueryable<RecipeRatings> recipesRatings)
    {
        return recipesRatings.Select(rec => new ListedRecipeRatingsDto
        {
            RecipeId = rec.RecipeId,
            RecipeName = rec.Recipe.Name,
            ImageSrc = rec.Recipe.ImageSrc,
            RatingNum = rec.Recipe.RatingNum,
            TotalRatings = rec.TotalRatings
        });
    }

    public static RecipeRatingsDto MapRecipeRatingsToDto(this RecipeRatings recipesRatings, Recipe recipe)
    {
        return new RecipeRatingsDto
        {
            RecipeId = recipesRatings.RecipeId,
            RecipeName = recipe!.Name,
            ImageSrc = recipe.ImageSrc,
            RatingNum = recipe.RatingNum,
            TotalRatings = recipesRatings.TotalRatings,
            Ratings = recipesRatings.Ratings != null ?
            recipe.RecipeRatings.Ratings.Select(rating => new RecipeRatingDto
            {
                RatingId = rating.Id,
                UserId = rating.UserId.ToString(),
                RatingNum = rating.RatingNum,
                Comment = rating.Comment,
            }).ToList()
            : [],
        };
    }
}