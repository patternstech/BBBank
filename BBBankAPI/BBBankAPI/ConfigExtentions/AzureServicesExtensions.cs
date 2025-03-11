using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class AzureServicesExtensions
{
    public static void ConfigureAzureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var signalRConnectionString = configuration["AzureSignalRConnectionString"];
        if (string.IsNullOrEmpty(signalRConnectionString))
        {
            throw new InvalidOperationException("AzureSignalRConnectionString is not set in configuration.");
        }

        services.AddSignalR().AddAzureSignalR(signalRConnectionString);
    }

}
