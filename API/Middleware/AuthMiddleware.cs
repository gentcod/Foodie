using System.Net;
using System.Text.Json;
using API.RequestHelpers;

namespace API.Middleware;
public class AuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        
        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var response = ApiErrorResponse.Response(
                "error",
                "You are not authorized to perform this action"
            );
            var options = GetOptions();
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
        else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var response = ApiErrorResponse.Response(
                "error",
                "Please login to perform this action"
            );
            var options = GetOptions();
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
        await _next(context);
    }

    private static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }
}