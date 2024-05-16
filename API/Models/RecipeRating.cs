namespace API.Models;
public class RecipeRating : Rating
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
