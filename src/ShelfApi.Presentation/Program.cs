using Serilog;
using ShelfApi.Application;
using ShelfApi.Infrastructure;
using ShelfApi.Presentation;
using ShelfApi.Presentation.Middlewares;
using ShelfApi.Presentation.SettingAggregate;
using ShelfApi.Presentation.Tools;
using System.Text.Json.Serialization;

StartupData startupDate = await ProjectInitializer.InitializeAsync();
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    IServiceCollection services = builder.Services;

    services.AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(o =>
    {
        o.DocumentFilter<SwaggerDocumentFilter>();
    });

    services.AddPresentation(startupDate.JwtSettings);
    services.AddInfrastructure(startupDate.ShelfApiDbConnectionString);
    services.AddApplication();

    WebApplication app = builder.Build();

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
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}