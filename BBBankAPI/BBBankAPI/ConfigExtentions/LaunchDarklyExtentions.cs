using Infrastructure;
using LaunchDarkly.Sdk.Server.Interfaces;
using LaunchDarkly.Sdk.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

public static class LaunchDarklyExtentions
{
    public static void ConfigureLaunchDarkly(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ILdClient>(provider =>
        {
            var config = configuration["launchDarklySdkKey"];
            return new LdClient(config);
        });
        services.AddScoped<FeatureService>();
    }
}
