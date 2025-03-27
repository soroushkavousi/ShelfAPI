using System.Diagnostics;
using DotNetPotion.AppEnvironmentPack;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
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
        services.AddSerilog(startupData);
        services.AddShelfApiDbContext(startupData);
        services.AddFusionCache(startupData);

        services.AddSingleton<IIdManager, IdManager>();
    }

    public static void AddSerilog(this IServiceCollection services, StartupData startupData)
    {
        services.AddSerilog((services, configuration) => configuration
            .Enrich.WithProperty("Application", "ShelfApi")
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers([new DbUpdateExceptionDestructurer()]))
            .Enrich.FromLogContext()
            .ReadFrom.Services(services)
            .WriteTo.Console(LogEventLevel.Warning)
            .WriteTo.Elasticsearch([new(startupData.Elasticsearch.Url)], opts =>
            {
                opts.DataStream = new("logs", nameof(ShelfApi), AppEnvironment.EnvironmentName);
                opts.BootstrapMethod = BootstrapMethod.Failure;
                opts.ConfigureChannel = channelOpts =>
                {
                    channelOpts.BufferOptions = new()
                    {
                        ExportMaxConcurrency = 10
                    };
                };
            }, transport =>
            {
                transport.Authentication(new ApiKey(startupData.Elasticsearch.ApiKey));
                transport.CertificateFingerprint(startupData.Elasticsearch.FingerPrint);
                transport.RequestTimeout(TimeSpan.FromSeconds(startupData.Elasticsearch.RequestTimeout));
            })
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Query", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Update", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Model.Validation", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
        );

        Log.Information("Elasticsearch logging configured successfully!");
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

    private static void AddFusionCache(this IServiceCollection services, StartupData startupData)
    {
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
                    Configuration = startupData.Redis.Configuration,
                    InstanceName = startupData.Redis.InstanceName
                }));
    }
}