using API.DTOs;
using API.Models;

namespace API.Extensions
{
   public static class RecipeRatingsExtension
    {
        public static IEnumerable<RecipeRatingsDto> MapRecipesRatingsToDto(this List<RatingRecipe> recipesRatings, List<Recipe> recipes)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipes.Find(el => el.Id == rec.RecipeId).Name,
                RecipeImgSrc = recipes.Find(el => el.Id == rec.RecipeId).ImageSrc,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });
        }

        //
        public static IEnumerable<RecipeRatingsDto> MapRecipeRatingsToDto(this List<RatingRecipe> recipesRatings, Recipe recipe)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RatingNum = rec.RatingNum,
                Comment = rec.Comment,
            });
        }

        public static IEnumerable<RecipeRatingsDto> MapRecipesRatingsAggregatorToDto(this List<RatingRecipe> recipesRatings, List<Recipe> recipes)
        {
            return recipesRatings.Select(rec => new RecipeRatingsDto
            {
                RatingId = rec.Id,
                RecipeName = recipes.Find(el => el.Id == rec.RecipeId).Name,
                RecipeImgSrc = recipes.Find(el => el.Id == rec.RecipeId).ImageSrc,
                RatingNum = rec.RatingNum,
            }).DistinctBy(rec => rec.RecipeName);
        }
    }
}