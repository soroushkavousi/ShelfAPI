using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Infrastructure.Data;

namespace ShelfApi.Infrastructure;

public static class ProjectInitializer
{
    public static void Initialize(EnvironmentName environmentName, string dbConnectionString)
    {
        CheckDbConnectionString(dbConnectionString);
        LoadConfigsFromTheDatabase(environmentName, dbConnectionString);
        ConfigSerilog();
        Console.WriteLine($"Project '{nameof(Infrastructure)}' has initialized successfully.");
    }

    private static void CheckDbConnectionString(string dbConnectionString)
    {
        if (string.IsNullOrWhiteSpace(dbConnectionString))
        {
            var message = "Please provide connection string!";
            Console.WriteLine($"Error: {message}");
            throw new Exception(message);
        }
    }

    private static void LoadConfigsFromTheDatabase(EnvironmentName environmentName, string dbConnectionString)
    {
        var dbContext = new ShelfApiDbContext(dbConnectionString);

        List<Configs> configs = dbContext.Configs
            .Where(e => e.EnvironmentName == environmentName)
            .AsNoTracking()
            .ToList();

        if (configs.Any())
            configs.ForEach(x => x.SetAsStatic());
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