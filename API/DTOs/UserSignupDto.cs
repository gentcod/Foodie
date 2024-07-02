using System.ComponentModel.DataAnnotations;

namespace API.RequestHelpers;
public class UserSignupDto
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 15 characters")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    public string MiddleName { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}