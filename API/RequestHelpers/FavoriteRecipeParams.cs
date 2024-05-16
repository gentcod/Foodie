using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.RequestHelpers;
public class FavoriteRecipeParams
{
    [BindRequired]
    public int RecipeId { get; set; }
}
