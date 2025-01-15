using Bitiano.Shared;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common.Extensions;
using ShelfApi.Domain.Common.Tools.Serializer;
using ShelfApi.Infrastructure.Data.ShelfApiDb;

namespace ShelfApi.Presentation;

public record StartupData
{
    private static readonly ProjectSettingId[] StartupSettingCategories =
    [
        ProjectSettingId.JWT
    ];

    private StartupData() { }

    public AppEnvironmentName EnvironmentName { get; private set; }
    public string ShelfApiDbConnectionString { get; private set; }
    public JwtSettings JwtSettings { get; private set; } = new("hj4EvE7fdZrliSByeJfD5vErKXkdIBSV", "http://localhost", "http://localhost");

    public static async Task<StartupData> InitializeAsync()
    {
        StartupData startupData = new();
        startupData.LoadFromEnvironmentVariables();
        await startupData.LoadFromDatabaseAsync();
        return startupData;
    }

    private void LoadFromEnvironmentVariables()
    {
        EnvironmentName = EnvironmentVariables.EnvironmentName.Value.ToEnum<AppEnvironmentName>()
            ?? AppEnvironmentName.DEVELOPMENT;

        ShelfApiDbConnectionString = EnvironmentVariables.ConnectionString.Value;
        if (string.IsNullOrWhiteSpace(ShelfApiDbConnectionString))
        {
            string message = "Please provide connection string!";
            Console.WriteLine($"Error: {message}");
            throw new ArgumentNullException(nameof(ShelfApiDbConnectionString), message);
        }
    }

    private async Task LoadFromDatabaseAsync()
    {
        ShelfApiDbContext shelfApiDbContext = new(ShelfApiDbConnectionString);
        try
        {
            Dictionary<ProjectSettingId, string> projectSettings = await shelfApiDbContext.ProjectSettings
                .Where(x => StartupSettingCategories.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Data);

            JwtSettings = projectSettings[ProjectSettingId.JWT].FromJson<JwtSettings>();
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Could not load ProjectSettings because of {ex.GetType().Name} {ex.Message}");
        }
        finally
        {
            await shelfApiDbContext.DisposeAsync();
        }
    }
}