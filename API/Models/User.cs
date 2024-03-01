using Microsoft.AspNetCore.Identity;

namespace API.Models
{
   public class User : IdentityUser<int>
    {     
        public string Name { get; set; }
        public string Password { get; set; }
    }
}