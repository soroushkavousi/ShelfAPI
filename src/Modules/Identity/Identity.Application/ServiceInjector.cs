using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Modules.Identity.Application.Services.TokenServicePack;

namespace ShelfApi.Modules.Identity.Application;

public static class ServiceInjector
{
    public static void AddIdentityApplication(this IServiceCollection services,
        Action<TokenServiceOptions> tokenServiceOptionsAction)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceInjector).Assembly));

        services.AddTokenService(tokenServiceOptionsAction);
    }
}