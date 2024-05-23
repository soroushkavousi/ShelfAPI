using ShelfApi.Domain.Common.Extensions;
using ShelfApi.Presentation.Tools;
using EnvironmentName = ShelfApi.Domain.ConfigurationAggregate.EnvironmentName;

namespace ShelfApi.Presentation;

public static class ProjectInitializer
{
    private static bool _initialized = false;

    public static void Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;
        EnvironmentName environmentName = EnvironmentVariables.EnvironmentName.Value.ToEnum<EnvironmentName>()
            ?? EnvironmentName.DEVELOPMENT;

        Infrastructure.ProjectInitializer.Initialize(environmentName, EnvironmentVariables.ConnectionString.Value);
        Console.WriteLine($"Project '{nameof(Presentation)}' has initialized successfully.");
    }
}