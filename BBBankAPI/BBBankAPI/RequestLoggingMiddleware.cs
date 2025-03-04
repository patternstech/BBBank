using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace BBBankAPI
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly TelemetryClient _telemetryClient;
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, TelemetryClient telemetryClient)
        {
            _next = next;
            _logger = logger;
            _telemetryClient = telemetryClient;
        }
        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;
            _logger.LogInformation($"Request started: {requestPath}");
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await _next(context); // Proceed with the next middleware/controller
                stopwatch.Stop();
                _logger.LogInformation($"Request completed: {requestPath} with status {context.Response.StatusCode} in {stopwatch.ElapsedMilliseconds} ms");

            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, $"Exception in {requestPath}");
                _telemetryClient.TrackException(ex);
                throw;
            }
        }
    }
}