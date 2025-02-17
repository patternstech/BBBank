using Infrastructure;
using Services.Contracts;

using Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Infrastructure.Contracts;
using Entites;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Identity.Web;
using AutoWrapper;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder => builder
                  .WithOrigins("http://localhost:4200", "Access-Control-Allow-Origin", "Access-Control-Allow-Credentials")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials()
       );
});

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddAzureAppConfiguration(options =>
             {
                options.Connect(builder.Configuration["ConnectionStrings:AzureAppConfigConnString"])
                   //.Select("BBBankFunctions*", LabelFilter.Null)
                   //.ConfigureRefresh(refresh =>
                   // {
                   //   refresh.Register("BBBankAPI:Settings:TestKey", refreshAll: true)
                   //      .SetCacheExpiration(new TimeSpan(0, 0, 30));
                   //})
                   .ConfigureKeyVault(kv =>
                   {
                       kv.SetCredential(new DefaultAzureCredential());
                   });
             })
             .Build();

var connectionString = configuration["BBBankDBConnString"];

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddAzureAppConfiguration();
builder.Services.Configure<Settings>(configuration.GetSection("BBBankAPI:Settings"));
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddScoped<DbContext, BBBankContext>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddDbContext<BBBankContext>(
b => b.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
.UseLazyLoadingProxies(false)
);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
app.UseApiResponseAndExceptionWrapper();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
