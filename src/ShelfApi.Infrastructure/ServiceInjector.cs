using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.Common;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Infrastructure.Tools;
using System.Diagnostics;

namespace ShelfApi.Infrastructure;

public static class ServiceInjector
{
    public static void AddInfrastructure(this IServiceCollection services, string shelfApiDbConnectionString)
    {
        services.AddSingleton<ISerializer, Serializer>();
        services.AddSingleton<IIdManager>(new IdManager(0));
        AddShelfApiDbContext(services, shelfApiDbConnectionString);
    }

    private static void AddShelfApiDbContext(this IServiceCollection services, string shelfApiDbConnectionString)
    {
        services.AddScoped<IShelfApiDbContext, ShelfApiDbContext>();
        services.AddDbContext<ShelfApiDbContext>(options =>
        {
            options.UseSqlServer(shelfApiDbConnectionString)
                .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
            options.EnableSensitiveDataLogging(true);
        });
    }
}