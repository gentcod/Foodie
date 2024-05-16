using API.DTOs;
using API.Models;

namespace API.Extensions;
public static class RecipeExtension
{
   public static IQueryable<Recipe> Search(this IQueryable<Recipe> query, string keyword)
   {
      if (string.IsNullOrEmpty(keyword)) return query.OrderBy(rec => rec.Id);

      var keywordLower = keyword.ToLower();

      return query.Where(rec => rec.Name.ToLower().Contains(keywordLower) || rec.Ingredients.ToLower().Contains(keywordLower));
   }

   public static IQueryable<Recipe> Sort(this IQueryable<Recipe> query, string sortBy)
   {
      if (string.IsNullOrEmpty(sortBy)) return query.OrderBy(rec => rec.Id);

      query = sortBy switch
      {
         "origin" => query.OrderBy(rec => rec.Origin),
         _ => query.OrderBy(rec => rec.Id)
      };

      return query;
   }

   public static IQueryable<Recipe> OrderByCookTime(this IQueryable<Recipe> query, int? orderBy)
   {
      if (orderBy == null) return query.OrderBy(rec => rec.Id);

      return query.OrderBy(rec => rec.CookTime);
   }

    public static IQueryable<Recipe> FilterByCategory(this IQueryable<Recipe> query, string category)
    {
        if (category == null) return query.OrderBy(rec => rec.Id);

        return query.Where(rec => (category != "foreign" ? rec.Category.ToLower().Contains(category) : !rec.Origin.ToLower().Contains("nigeria")));
    }

    public static IQueryable<Recipe> Featured(this IQueryable<Recipe> query)
   {
      return query.Where(rec => rec.Featured == true);
   }

   public static IQueryable<ListedRecipeDto> MapRecipesToDto(this IQueryable<Recipe> recipes)
   {
      return recipes.Select(rec => new ListedRecipeDto
      {
         Id = rec.Id,
         Name = rec.Name,
         ImageSrc = rec.ImageSrc,
         CookTime = $"{rec.CookTime} mins",
         Origin = rec.Origin,
         Rating = rec.Rating,
         Category = rec.Category,
         Featured = rec.Featured,
      });
   }

   public static RecipeDto MapRecipeToDto(this Recipe recipe)
   {
      return new RecipeDto
      {
         Id = recipe.Id,
         Name = recipe.Name,
         Ingredients = recipe.Ingredients,
         Description = recipe.Description,
         ImageSrc = recipe.ImageSrc,
         CookTime = $"{recipe.CookTime} mins",
         DateAdded = recipe.DateAdded.ToString("dddd, dd MMMM yyyy"),
         Origin = recipe.Origin,
         Rating = recipe.Rating,
         Category = recipe.Category,
         Featured = recipe.Featured,
      };
   }
}
