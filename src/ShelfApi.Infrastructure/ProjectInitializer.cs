using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Infrastructure.Data;

namespace ShelfApi.Infrastructure;

public static class ProjectInitializer
{
    public static void Initialize(EnvironmentName environmentName, string dbConnectionString)
    {
        CheckDbConnectionString(dbConnectionString);
        LoadConfigsFromTheDatabase(environmentName, dbConnectionString);
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
}
