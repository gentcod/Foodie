using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.RequestHelpers;
public class BookmarkParams
{
    [BindRequired]
    public int RecipeId { get; set; }
}
