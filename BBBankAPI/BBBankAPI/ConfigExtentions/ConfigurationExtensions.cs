using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;

public static class ConfigurationExtensions
{
    public static void ConfigureAppConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddAzureAppConfiguration(options =>
            {
                options.Connect(builder.Configuration["ConnectionStrings:AzureAppConfigConnString"])
                       .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()));
            });
    }
}
