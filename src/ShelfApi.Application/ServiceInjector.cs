﻿using Bitiano.DevKit.Services.TaskServices;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.BaseDataApplication.Services;
using ShelfApi.Application.Common.Tools.MediatR;
using System.Reflection;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IBaseDataService, BaseDataService>();
        services.AddTaskService();
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