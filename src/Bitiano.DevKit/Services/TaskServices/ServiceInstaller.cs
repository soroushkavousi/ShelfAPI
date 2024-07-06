using Microsoft.Extensions.DependencyInjection;

namespace Bitiano.DevKit.Services.TaskServices;

public static class ServiceInstaller
{
    public static IServiceCollection AddTaskService(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, TaskService>();
        return services;
    }
}