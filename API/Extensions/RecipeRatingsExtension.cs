using API.DTOs;
using API.Models;

namespace API.Extensions
{
   public static class RecipeRatingsExtension
    {
        public static IEnumerable<RecipeRatingsDto> MapRecipesRatingsToDto(this List<RecipeRating> recipesRatings, List<Recipe> recipes)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipes.Find(el => el.Id == rec.RecipeId).Name,
                RecipeImgSrc = recipes.Find(el => el.Id == rec.RecipeId).ImageSrc,
                Rating = recipes.Find(el => el.Id == rec.RecipeId).Rating,
                Comment = rec.Comment,
            });
        }

        //
        public static IEnumerable<RecipeRatingsDto> MapRecipeRatingsToDto(this List<RecipeRating> recipesRatings, Recipe recipe)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipe.Name,
                RecipeImgSrc = recipe.ImageSrc,
                Rating = recipe.Rating,
                Comment = rec.Comment,
            });
        }

        public static IEnumerable<RecipeRatingsDto> MapRecipesRatingsAggregatorToDto(this List<RecipeRating> recipesRatings, List<Recipe> recipes)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipes.Find(el => el.Id == rec.RecipeId).Name,
                RecipeImgSrc = recipes.Find(el => el.Id == rec.RecipeId).ImageSrc,
                Rating = recipes.Find(el => el.Id == rec.RecipeId).Rating,
            }).DistinctBy(rec => rec.RecipeName);
        }
    }
}