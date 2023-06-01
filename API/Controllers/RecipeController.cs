using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   public class RecipeController : BaseApiController
    {
        [HttpGet]
        public  ActionResult GetRecipes()
        {
            return Ok();
        }
    }
}