using API.Data;
using API.DTOs;
using API.Models;
using API.RequestHelpers;
using API.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AccountController(FoodieContext context, UserManager<User> userManager, JwtTokenGenerator tokenGenerator) : BaseApiController
{
    private readonly FoodieContext _context = context;
    private readonly UserManager<User> _usermanager = userManager;
    private readonly JwtTokenGenerator _tokenGenerator = tokenGenerator;

    [HttpPost("login")]
    public async Task<ActionResult<object>> Login(UserLoginDto loginDto)
    {
        var user = await _usermanager.FindByEmailAsync(loginDto.Email);
        var authenticated = await _usermanager.CheckPasswordAsync(user, loginDto.Password);

        if (user == null || !authenticated) return Unauthorized(new ProblemDetails { Title = "User account does not exist" });

        var token = await _tokenGenerator.CreateToken(user);
        var userDto = new UserDto
        {
            Name = user.Name,
            Username = user.UserName,
            Email = user.Email,
        };

        return new
        {
            AccessToken = token,
            User = userDto,
        };
    }
}