using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.RequestHelpers
{
   public class BookmarkParams
    {
        public string UserId { get; set; }
        [BindRequired]
        public int RecipeId { get; set; }
    }
}