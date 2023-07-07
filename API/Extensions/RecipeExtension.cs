using API.DTOs;
using API.Entities;

namespace API.Extensions
{
   public static class RecipeExtension
   {
      public static IQueryable<Recipe> Search(this IQueryable<Recipe> query, string keyword)
      {
         if (string.IsNullOrEmpty(keyword)) return query;

         var keywordLower = keyword.ToLower();

         return query.Where(rec => rec.Name.ToLower().Contains(keywordLower));
      }

      public static IQueryable<Recipe> Sort(this IQueryable<Recipe> query, string sortBy)
      {
         if (string.IsNullOrEmpty(sortBy)) return query.OrderBy(rec => rec.Name);

         query = sortBy switch
         {
            "origin" => query.OrderBy(rec => rec.Origin),
            _ => query.OrderBy(rec => rec.Name)
         };

         return query;
      }

      public static IQueryable<Recipe> OrderByCookTime(this IQueryable<Recipe> query, int? orderBy)
      {
         if (orderBy == null) return query.OrderBy(rec => rec.Name);

         query = query.OrderBy(rec => rec.CookTime);

         return query;
      }

      public static List<RecipeDto> MapRecipeToDto(this List<Recipe> recipes)
      {
         return recipes.Select(rec => new RecipeDto
         {
            Id = rec.Id,
            Name = rec.Name,
            Ingredients = rec.Ingredients,
            Description = rec.Description,
            CookTime = $"{rec.CookTime.ToString()} mins",
            DateAdded = rec.DateAdded.ToString("dddd, dd MMMM yyyy"),
            Origin = rec.Origin,
            RatingRecipe = rec.RatingRecipe,
         }).ToList();
      }
   }
}