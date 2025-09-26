using Microsoft.Extensions.DependencyInjection;
using ShelfApi.IdentityModule.Application.Services.TokenServicePack;

namespace ShelfApi.IdentityModule.Application;

public static class ServiceInjector
{
    public static void AddIdentityApplication(this IServiceCollection services,
        Action<TokenServiceOptions> tokenServiceOptionsAction)
    {
        services.AddTokenService(tokenServiceOptionsAction);
    }
}