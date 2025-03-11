using AutoWrapper;
using BBBankAPI;
using Microsoft.AspNetCore.Builder;
using Services;

public static class MiddlewareExtensions
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseWhen(
            context => !context.Request.Path.StartsWithSegments("/api/graphUpdates"),
            branch => { branch.UseApiResponseAndExceptionWrapper(); }
        );
    }

    public static void ConfigureRouting(this WebApplication app)
    {
        app.UseRouting();
        app.UseCors("_myAllowSpecificOrigins");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<UpdateHub>("/api/graphUpdates");
        });
    }
}
