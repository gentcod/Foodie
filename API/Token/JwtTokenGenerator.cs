using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace API.Token
{
    public class JwtTokenGenerator(UserManager<User> userManager, IConfiguration config)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _config = config;

        public async Task<string> CreateToken(User user) 
        {
            var payload = new Payload
            {
                Id = Guid.NewGuid(),
                Username = user.UserName,
                Email = user.Email
            };

            //claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new("payload", JsonConvert.SerializeObject(payload))
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Payload VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var payloadStr = jsonToken.Claims.FirstOrDefault(el => el.Type == "payload").Value;
            var payload = JsonConvert.DeserializeObject(payloadStr) as Payload;

            return payload;
        }
    }
}