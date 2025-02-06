using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Tools;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Infrastructure.Tools;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace ShelfApi.Infrastructure;

public static class ServiceInjector
{
    public static void AddInfrastructure(this IServiceCollection services, StartupData startupData)
    {
        services.AddSingleton<IIdManager, IdManager>();
        AddShelfApiDbContext(services, startupData);
        services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(2),

                DistributedCacheDuration = TimeSpan.FromHours(1),
                DistributedCacheSoftTimeout = TimeSpan.FromSeconds(5),
                DistributedCacheHardTimeout = TimeSpan.FromSeconds(20),

                FactorySoftTimeout = TimeSpan.FromMinutes(1),
                FactoryHardTimeout = TimeSpan.FromMinutes(2),

                AllowBackgroundDistributedCacheOperations = true,

                JitterMaxDuration = TimeSpan.FromSeconds(2)
            })
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .WithDistributedCache(
                new RedisCache(new RedisCacheOptions
                {
                    Configuration = startupData.RedisConfiguration,
                    InstanceName = startupData.RedisInstanceName
                }));
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