using Mapster;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.BaseDataApplication.Services;
using ShelfApi.Application.ErrorApplication;
using System.Reflection;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IBaseDataService, BaseDataService>();
        services.AddScoped<TokenService>();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        AddHostedServices(services);
    }

    private static void AddHostedServices(IServiceCollection services)
    {
        services.AddHostedService<BaseDataHostedService>();
    }
}