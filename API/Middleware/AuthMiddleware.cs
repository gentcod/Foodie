using System.Net;
using System.Text.Json;
using API.Token;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;
public class AuthMiddleware : IMiddleware
{
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly IHostEnvironment _env;

    private string AuthHeaderKey { get; set; } = "Authorization";
    private string AuthTypeBearer { get; set; } = "bearer";
    private string AuthPayloadKey { get; set; } = "Auth_Payload";

    public AuthMiddleware(JwtTokenGenerator tokenGenerator, IHostEnvironment env)
    {
        _tokenGenerator = tokenGenerator;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            string authHeader = context.Request.Headers[AuthHeaderKey];

            if (string.IsNullOrEmpty(authHeader))
            {
                ReturnErrorMessage("authorization must be provided", context);
                return;
            }

            var authFields = authHeader.Split(' ');

            if (authFields.Count() < 2)
            {
                ReturnErrorMessage("invalid Authorization header format", context);
                return;
            }


            if (authFields[0].ToLower() != AuthTypeBearer)
            {
                ReturnErrorMessage($"unsuppoerted authorization type {authFields[0]}", context);
                return;
            }

            var accessToken = authFields[1];
            //var claimsPrincipal = _tokenGenerator.VerifyToken(accessToken);
            //await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var response = new ProblemDetails
            {
                Detail = _env.IsDevelopment() ? ex.StackTrace.ToString() : null,
                Title = ex.Message
            };

            var options = GetOptions();

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }

    }

    private static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    private static async void ReturnErrorMessage(string message, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        var response = new ProblemDetails
        {
            Detail = message
        };
        var options = GetOptions();
        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}