using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Models;
using API.RequestHelpers;
using API.Token;
using Microsoft.AspNetCore.Authorization;
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
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName} {user.MiddleName}",
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
            UserId = Guid.NewGuid(),
            Email = userSignupDto.Email,
            UserName = userSignupDto.Username,
            FirstName = userSignupDto.FirstName,
            LastName = userSignupDto.LastName,
            MiddleName = userSignupDto.MiddleName,
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

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult> GetProfie()
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        var bookmarks = await _context.Bookmarks.FirstOrDefaultAsync(bookmark => bookmark.UserId == GetUserId());
        var favorites = await _context.Favorites.FirstOrDefaultAsync(bookmark => bookmark.UserId == GetUserId());

        var profileDto = new UserProfileDto
        {
            Username = user.UserName,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName} {user.MiddleName}",
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            TotalBookmarks = bookmarks != null ? bookmarks.TotalBookmarks : 0,
            TotalFavRecipes = favorites != null ? favorites.TotalFavRecipes : 0,
            TotalFavRestaurants = favorites != null ? favorites.TotalFavRestaurants : 0,
        };

        return Ok(ApiSuccessResponse<UserProfileDto>.Response(
            "success",
            "Profile fetched successfully",
            profileDto
        ));
    }

    [Authorize]
    [HttpGet("session")]
    public async Task<ActionResult> CheckSession()
    {
        var user = await _context.Users.FirstOrDefaultAsync(el => el.UserId == GetUserId());
        if (user != null) return Ok(ApiSuccessResponse<object>.Response(
            "success",
            "User is authenticated",
            null
        ));

        return Unauthorized(ApiErrorResponse.Response(
            "error",
            "User account does not exist"
        ));
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        _ = Guid.TryParse(userIdClaim.Value, out var userId);
        return userId;
    }
}