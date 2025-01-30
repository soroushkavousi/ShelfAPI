using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Tools;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Infrastructure.Tools;

namespace ShelfApi.Infrastructure;

public static class ServiceInjector
{
    public static void AddInfrastructure(this IServiceCollection services, StartupData startupData)
    {
        services.AddSingleton<IIdManager, IdManager>();
        AddShelfApiDbContext(services, startupData);
    }

    private static void AddShelfApiDbContext(this IServiceCollection services, StartupData startupData)
    {
        services.AddScoped<IShelfApiDbContext, ShelfApiDbContext>();
        services.AddDbContext<ShelfApiDbContext>(options =>
        {
            options.UseNpgsql(startupData.DbConnectionString)
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
            options.EnableSensitiveDataLogging();
        });
    }
}