using System.Reflection;
using DotNetPotion.ScopeServicePack;
using DotNetPotion.SemaphorePoolPack;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.AuthApplication.Services;
using ShelfApi.Application.Common.Tools.MediatR;
using ShelfApi.Application.ProductApplication.Services;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScopeService();
        services.AddScoped<TokenService>();
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorResultPipelineBehavior<,>));
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddSemaphorePool();

        AddHostedServices(services);
    }

    private static void AddHostedServices(IServiceCollection services)
    {
        services.AddHostedService<SyncProductElasticDocumentBackgroundService>();
    }
}