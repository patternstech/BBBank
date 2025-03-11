using Microsoft.Extensions.DependencyInjection;

public static class CorsExtensions
{
    private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins, builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
    }

    public static void UseCorsPolicy(this WebApplication app)
    {
        app.UseCors(MyAllowSpecificOrigins);
    }
}
