using System.Text.Json;

namespace diversa.Middleware;

public class JsonResponseMiddleware
{
    private readonly RequestDelegate _next;

    public JsonResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsSwaggerRequest(context.Request.Path))
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        await _next(context);
        var acceptsJson = context.Request.Headers.Accept.Any(h => h?.Contains("application/json") == true);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
        context.Response.Body = originalBodyStream;

        if (acceptsJson)
        {
            try
            {
                JsonDocument.Parse(responseBody);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseBody);
            }
            catch (JsonException)
            {
                var jsonResponse = JsonSerializer.Serialize(new { content = responseBody });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);
            }
        }
        else
        {
            await context.Response.WriteAsync(responseBody);
        }
    }

    private static bool IsSwaggerRequest(PathString path)
    {
        return !path.StartsWithSegments("/api");
    }
}