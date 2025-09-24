using Microsoft.Extensions.DependencyInjection;

namespace ShelfApi.Modules.IdentityModule.Application.Services.TokenServicePack;

public static class ServiceInjector
{
    public static void AddTokenService(this IServiceCollection services,
        Action<TokenServiceOptions> tokenServiceOptionsAction)
    {
        services.Configure(tokenServiceOptionsAction);
        services.AddScoped<TokenService>();
    }
}