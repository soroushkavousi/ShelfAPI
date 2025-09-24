using System.Reflection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using ShelfApi.Application;
using ShelfApi.Application.Common.Data;
using ShelfApi.Infrastructure;
using ShelfApi.Modules.IdentityModule.Infrastructure;
using ShelfApi.Presentation;
using ShelfApi.Presentation.Middlewares;
using ServiceInjector = ShelfApi.Presentation.ServiceInjector;

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
        .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
            .WithDefaultDestructurers()
            .WithDestructurers([new DbUpdateExceptionDestructurer()]))
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
        .CreateBootstrapLogger();
}

static async Task StartAppAsync(string[] args)
{
    StartupData startupData = await ServiceInjector.ReadStartupData();

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    ConfigureBuilder(builder, startupData);

    WebApplication app = builder.Build();
    ConfigureApp(app);

    app.Run();
}

static void ConfigureBuilder(WebApplicationBuilder builder, StartupData startupData)
{
    ConfigureServices(builder.Services, startupData);
}

static void ConfigureServices(IServiceCollection services, StartupData startupData)
{
    services.AddSingleton(startupData);
    services.AddPresentation(startupData);
    services.AddInfrastructure(startupData);
    services.AddApplication();
    AddModules(services, startupData);
}

static void AddModules(IServiceCollection services, StartupData startupData)
{
    services.AddIdentityModule(tokenServiceOptions =>
    {
        tokenServiceOptions.JwtKey = startupData.Jwt.Key;
        tokenServiceOptions.JwtIssuer = startupData.Jwt.Issuer;
        tokenServiceOptions.JwtAudience = startupData.Jwt.Audience;
    });
}

static void ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.UseApiExceptionHandler();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

static Assembly[] GetModuleAssemblies()
{
    return AppDomain.CurrentDomain
        .GetAssemblies()
        .Where(a => a.FullName.Contains("Module"))
        .ToArray();
}