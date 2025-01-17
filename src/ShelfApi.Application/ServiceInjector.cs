﻿using System.Reflection;
using DotNetPotion.Services.ScopedTaskRunner;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.AuthApplication.Services;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.BaseDataApplication.Services;
using ShelfApi.Application.Common.Tools.MediatR;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IBaseDataService, BaseDataService>();
        services.AddScopedTaskRunner();
        services.AddScoped<TokenService>();
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorResultPipelineBehavior<,>));
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        AddHostedServices(services);
    }

    private static void AddHostedServices(IServiceCollection services)
    {
        services.AddHostedService<BaseDataHostedService>();
    }
}