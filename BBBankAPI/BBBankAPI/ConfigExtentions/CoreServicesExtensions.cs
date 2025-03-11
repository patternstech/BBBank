using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;
using Infrastructure;
using Infrastructure.Contracts;
using Entites;
using Microsoft.EntityFrameworkCore;

public static class ServiceExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAccountsService, AccountsService>();
        services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
        services.AddScoped<DbContext, BBBankContext>();
        services.AddScoped<IRulesEngineService, RulesEngineService>();
    }
}
