namespace API.DTOs;
public class UserProfileDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public int TotalBookmarks { get; set; }
    public int TotalFavRecipes { get; set; }
    public int TotalFavRestaurants { get; set; }
}
