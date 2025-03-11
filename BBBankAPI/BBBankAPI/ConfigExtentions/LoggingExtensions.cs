using Microsoft.Extensions.Hosting;
using Serilog;

public static class LoggingExtensions
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.AddSerilog(logger);
        builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
    }
}
