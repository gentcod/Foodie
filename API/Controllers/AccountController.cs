using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Models;
using API.RequestHelpers;
using API.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class AccountController(FoodieContext context, UserManager<User> userManager, JwtTokenGenerator tokenGenerator) : BaseApiController
{
    private readonly FoodieContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private readonly JwtTokenGenerator _tokenGenerator = tokenGenerator;

    [HttpPost("login")]
    public async Task<ActionResult<ApiSuccessResponse<object>>> Login(UserLoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(ApiErrorResponse.Response(
            "error",
            "User account does not exist"
        ));

        var authenticated = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!authenticated) return Unauthorized(ApiErrorResponse.Response(
            "error",
            "Wrong credentials."
        ));

        var token = await _tokenGenerator.CreateToken(user);
        var userDto = new UserDto
        {
            Username = user.UserName,
            Name = user.Name,
            Email = user.Email,
        };

        var data = new
        {
            AccessToken = token,
            User = userDto,
        };

        return Ok(ApiSuccessResponse<object>.Response(
            "success",
            "Account logged in successfully",
            data
        ));
    }

    [HttpPost("signup")]
    public async Task<ActionResult> Signup(UserSignupDto userSignupDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(userSignupDto.Email);

        if (existingUser != null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "User account already exists. Please login"
        ));

        var existingUserName = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userSignupDto.Username.ToLower());

        if (existingUserName != null) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Username already taken. Please select a different username"
        ));

        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
            UserName = userSignupDto.Username,
            Name = $"{userSignupDto.FirstName} {userSignupDto.LastName}",
            Email = userSignupDto.Email,
        };

        var result = await _userManager.CreateAsync(user, userSignupDto.Password);
        if (!result.Succeeded) return BadRequest(ApiErrorResponse.Response(
            "error",
            "Validation issues creating account"
        ));
    
        await _userManager.AddToRoleAsync(user, "Member");

        return StatusCode(201, ApiSuccessResponse<object>.Response(
            "success",
            "Account created successfully",
            null
        ));
    }
}