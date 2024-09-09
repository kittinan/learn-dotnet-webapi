using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Call the next middleware in the pipeline
        await _next(context);

        stopwatch.Stop();

        // Log the time taken to process the request
        var elapsedTime = stopwatch.ElapsedMilliseconds;
        _logger.LogInformation($"Request [{context.Request.Method}] {context.Request.Path} took {elapsedTime} ms");
    }
}
