using Microsoft.AspNetCore.Identity;

namespace API.Models;
public class User : IdentityUser<int>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
}

