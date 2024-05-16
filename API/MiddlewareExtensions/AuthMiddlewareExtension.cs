using API.Middleware;

namespace API.MiddlewareExtensions;
public static class AuthMiddlewareExtension
{
    public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthMiddleware>();
    }
}