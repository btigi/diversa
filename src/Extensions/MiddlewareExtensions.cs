using diversa.Middleware;

namespace diversa.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseJsonResponseMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JsonResponseMiddleware>();
    }

    public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<BasicAuthMiddleware>();
    }
} 