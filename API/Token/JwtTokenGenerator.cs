using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Token
{
    public class JwtTokenGenerator(UserManager<User> userManager, IConfiguration config)
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IConfiguration _config = config;

        public async Task<string> CreateToken(User user) 
        {
            //claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserId),
                new(ClaimTypes.Expiration, DateTime.Now.AddHours(12).ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        //public ClaimsPrincipal VerifyToken(string token)
        //{
        //    try
        //    {
        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:TokenKey"]))
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var principalClaims = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                
        //        return principalClaims;
        //    }
        //    catch (SecurityTokenExpiredException)
        //    {
        //        throw new Exception("Expired token");
        //    }
        //    catch (SecurityTokenInvalidSignatureException)
        //    {
        //        throw new Exception("Invalid token signature");
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Token validation failed");
        //    }
        //}
    }
}