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
using AutoMapper;
using BBBankAPI;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Events;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API V1", Version = "v1" });
    c.SwaggerDoc("v2", new() { Title = "API V2", Version = "v2" });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

        // Get the versions mapped to the action method
        var actionVersions = methodInfo
            .GetCustomAttributes(true)
            .OfType<MapToApiVersionAttribute>()
            .SelectMany(attr => attr.Versions)
            .ToList();

        // If no versions are mapped to the action, assume it belongs to the default version (v1)
        if (!actionVersions.Any())
        {
            return docName == "v1";
        }

        // Otherwise, check if the action is mapped to the specified version
        return actionVersions.Any(v => $"v{v.MajorVersion}" == docName);
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default version
    options.AssumeDefaultVersionWhenUnspecified = true; // Assume default if not specified
    options.ReportApiVersions = true; // Report supported versions in the response header
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // Read version from the URL segment
});
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddApplicationInsightsTelemetry();

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
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});
builder.Services.AddDbContext<BBBankContext>(
b => b.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
.UseLazyLoadingProxies(false)
);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile(new MappingProfiles());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
var app = builder.Build();

// **Add Swagger Middleware Here**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
  {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "BBBank API v1");
      c.SwaggerEndpoint("/swagger/v2/swagger.json", "BBBank API v2");


  });

}

app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
app.UseApiResponseAndExceptionWrapper();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
