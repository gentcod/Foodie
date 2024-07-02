using Microsoft.AspNetCore.Identity;

namespace API.Models;
public class User : IdentityUser<int>
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
}

