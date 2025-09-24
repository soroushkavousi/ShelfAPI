using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Modules.IdentityModule.Application.Services.TokenServicePack;

namespace ShelfApi.Modules.IdentityModule.Application;

public static class ServiceInjector
{
    public static void AddIdentityApplication(this IServiceCollection services,
        Action<TokenServiceOptions> tokenServiceOptionsAction)
    {
        services.AddTokenService(tokenServiceOptionsAction);
    }
}