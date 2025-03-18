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
using Microsoft.Extensions.DependencyInjection;
using BBBankAPI.ConfigExtentions;
using Microsoft.ApplicationInsights.DependencyCollector;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.ConfigureSwagger();
builder.ConfigureLogging();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, options) =>
{
    module.EnableSqlCommandTextInstrumentation = true;
});
builder.Services.AddCorsPolicy();
builder.ConfigureAppConfiguration();
builder.Services.ConfigureAzureServices(builder.Configuration);
builder.Services.ConfigureServiceBus(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAzureAppConfiguration();
builder.Services.Configure<Settings>(configuration.GetSection("BBBankAPI:Settings"));
builder.Services.AddControllers();
builder.Services.RegisterApplicationServices();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd");
builder.Services.ConfigureAutoMapper();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
var app = builder.Build();
app.ConfigureSwaggerUI();

app.ConfigureMiddlewares();
app.ConfigureRouting();



app.Run();
