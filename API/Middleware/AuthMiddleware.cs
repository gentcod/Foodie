using System.Net;
using System.Text.Json;
using API.RequestHelpers;

namespace API.Middleware;
public static class AuthMiddleware
{
    public static async Task InvokeAsync(HttpContext context)
    {

        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            var response = ApiErrorResponse.Response(
                "error",
                "You are not authorized to perform this action"
            );
            var json = JsonSerializer.Serialize(response, GetOptions());
            await context.Response.WriteAsync(json);
        }
        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            var response = ApiErrorResponse.Response(
                "error",
                "Please login to perform this action"
            );
            var json = JsonSerializer.Serialize(response, GetOptions());
            await context.Response.WriteAsync(json);
        }
        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            // context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var response = ApiErrorResponse.Response(
                "error",
                "This route doesn't exist on the server"
            );
            var json = JsonSerializer.Serialize(response, GetOptions());
            await context.Response.WriteAsync(json);
        }
    }

    private static JsonSerializerOptions GetOptions()
    {
        return new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }
}