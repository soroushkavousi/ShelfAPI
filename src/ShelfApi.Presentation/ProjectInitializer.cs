using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using ShelfApi.Domain.Common;
using ShelfApi.Infrastructure.Tools;
using ShelfApi.Presentation.SettingAggregate;

namespace ShelfApi.Presentation;

public static class ProjectInitializer
{
    public static async Task<StartupSettings> InitializeAsync()
    {
        StartupSettings startupSettings = await StartupSettings.InitializeAsync();
        ConfigSerilog();
        Log.Information("startupSettings: {startupSettings}", startupSettings.ToJson());
        return startupSettings;
    }

    private static void ConfigSerilog()
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
}