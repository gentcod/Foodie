using System.Net;
using System.Text.Json;
using API.RequestHelpers;

namespace API.Middleware;
public class AuthMiddleware(IHostEnvironment env): IMiddleware
{
    private readonly IHostEnvironment _env = env;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
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
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var response = ApiErrorResponse.Response(
                "error",
                _env.IsDevelopment() ? ex.StackTrace.ToString() : "This is on us, we are working to resolve the problem"
            );
            var options = GetOptions();
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }

    }

    private static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }
}