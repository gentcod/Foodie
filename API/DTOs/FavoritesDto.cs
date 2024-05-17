namespace API.DTOs;

public class FavoritesDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int TotalFavRecipes { get; set; }
    public int TotalFavRestaurants { get; set; }
    public List<EmbeddedDto> Recipes { get; set; }
    public List<EmbeddedDto> Restaurants { get; set; }
}
