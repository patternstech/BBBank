using Infrastructure;
using Services.Contracts;

using Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Infrastructure.Contracts;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200");
                      });
});

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
                .Build();

var connectionString = configuration.GetConnectionString("BBBankDBConnString");

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));

builder.Services.AddScoped<DbContext, BBBankContext>();

builder.Services.AddDbContext<BBBankContext>(
b => b.UseSqlServer(connectionString , options => options.EnableRetryOnFailure())
.UseLazyLoadingProxies(false)
);
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
