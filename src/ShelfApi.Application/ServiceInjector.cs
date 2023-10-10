using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.ErrorApplication;
using System.Reflection;

namespace ShelfApi.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<TokenService>();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>));
    }
}
