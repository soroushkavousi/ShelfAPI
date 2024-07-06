using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using ShelfApi.Application;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.Infrastructure;
using ShelfApi.Presentation;
using ShelfApi.Presentation.ActionFilters;
using ShelfApi.Presentation.Middlewares;
using ShelfApi.Presentation.SettingAggregate;
using ShelfApi.Presentation.Tools;
using System.Text.Json.Serialization;

ConfigureBootstrapSerilog();
try
{
    Log.Warning("Starting App...");
    await StartAppAsync(args);
    Log.Warning("App stopped.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "App terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static void ConfigureBootstrapSerilog()
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.WithProperty("Application", "ShelfApi")
        .WriteTo.Seq("http://localhost:5341")
        .WriteTo.Console(LogEventLevel.Warning)
        .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Query", LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Update", LogEventLevel.Error)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Model.Validation", LogEventLevel.Error)
        .CreateLogger();
}

static async Task StartAppAsync(string[] args)
{
    StartupData startupData = await StartupData.InitializeAsync();

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    ConfigureBuilder(builder, startupData);

    WebApplication app = builder.Build();
    ConfigureApp(app, builder.Configuration);

    app.Run();
}

static void ConfigureBuilder(WebApplicationBuilder builder, StartupData startupData)
{
    builder.Host.UseSerilog();

    ConfigureServices(builder.Services, builder.Configuration, startupData);
}

static void ConfigureServices(IServiceCollection services, IConfiguration configuration, StartupData startupData)
{
    services.AddControllers(o =>
    {
        o.Filters.Add<StatusCodeActionFilter>();
    })
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonNumberEnumConverter<ErrorCode>());
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(o =>
    {
        o.DocumentFilter<SwaggerDocumentFilter>();
    });

    services.AddPresentation(startupData.JwtSettings);
    services.AddInfrastructure(startupData.ShelfApiDbConnectionString);
    services.AddApplication();
}

static void ConfigureApp(WebApplication app, IConfiguration configuration)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();
    app.UseApiExceptionHandler();
    app.UseAuthorization();
    app.UseAuthorization();
    app.MapControllers();
}