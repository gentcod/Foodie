using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "error",
                Detail = _env.IsDevelopment() ? ex.StackTrace.ToString() : "This is on us, we are working to resolve the problem",
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsJsonAsync(json);
        }
    }
}
