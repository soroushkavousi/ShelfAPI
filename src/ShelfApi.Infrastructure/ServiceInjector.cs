using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Tools;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Infrastructure.Tools;
using ShelfApi.Infrastructure.Tools.Serializers;

namespace ShelfApi.Infrastructure;

public static class ServiceInjector
{
    public static void AddInfrastructure(this IServiceCollection services, string shelfApiDbConnectionString)
    {
        services.AddSingleton<ISerializer, Serializer>();
        services.AddSingleton<IIdManager, IdManager>();
        AddShelfApiDbContext(services, shelfApiDbConnectionString);
    }

    private static void AddShelfApiDbContext(this IServiceCollection services, string shelfApiDbConnectionString)
    {
        services.AddScoped<IShelfApiDbContext, ShelfApiDbContext>();
        services.AddDbContext<ShelfApiDbContext>(options =>
        {
            options.UseNpgsql(shelfApiDbConnectionString)
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
            options.EnableSensitiveDataLogging();
        });
    }
}