using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class RecipeController : BaseApiController
   {
      private readonly RecipesContext _context;
      public RecipeController(RecipesContext context)
      {
         _context = context;
      }
      [HttpGet]
      public ActionResult GetRecipes()
      {
        var recipes = _context.Recipes;
        return Ok(recipes);
      }
   }
}