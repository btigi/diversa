using System.Text;

namespace diversa.Middleware;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public BasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsSwaggerRequest(context.Request.Path))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization header missing");
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid authorization scheme");
            return;
        }

        var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        var parts = credentials.Split(':', 2);

        if (parts.Length != 2)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid authorization header format");
            return;
        }

        var username = parts[0];
        var password = parts[1];

        var validUsername = _configuration["ApiCredentials:Username"];
        var validPassword = _configuration["ApiCredentials:Password"];

        if (username != validUsername || password != validPassword)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid credentials");
            return;
        }

        await _next(context);
    }

    private static bool IsSwaggerRequest(PathString path)
    {
        return !path.StartsWithSegments("/api");
    }
}