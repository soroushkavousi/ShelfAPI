using DotNetPotion.ScopeServicePack;
using DotNetPotion.SemaphorePoolPack;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.Common.Services;
using ShelfApi.Application.Common.Tools.MediatR;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScopeService();
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorResultPipelineBehavior<,>));

        services.AddSemaphorePool();

        AddHostedServices(services);
    }

    private static void AddHostedServices(IServiceCollection services)
    {
        services.AddHostedService<DomainEventOutboxProcessorBackgroundService>();
    }
}