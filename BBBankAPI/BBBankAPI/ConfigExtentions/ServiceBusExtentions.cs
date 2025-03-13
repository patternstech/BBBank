using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;

namespace BBBankAPI.ConfigExtentions
{
    public static class ServiceBusExtentions
    {
        public static void ConfigureServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["BBBankServiceBusConnectionString"];
            string topicName = configuration["BBBankServiceBusTopic"];

            services.AddScoped<INotificationService>(_ => new NotificationService(connectionString, topicName));
        }

    }
}
