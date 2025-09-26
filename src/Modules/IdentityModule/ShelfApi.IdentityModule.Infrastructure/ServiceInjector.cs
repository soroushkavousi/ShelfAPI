using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.IdentityModule.Application;
using ShelfApi.IdentityModule.Application.Services.TokenServicePack;
using ShelfApi.IdentityModule.Infrastructure.Configurations;

namespace ShelfApi.IdentityModule.Infrastructure;

public static class ServiceInjector
{
    public static void AddIdentityModule(this IServiceCollection services,
        Action<TokenServiceOptions> tokenServiceOptionsAction)
    {
        services.AddIdentityApplication(tokenServiceOptionsAction);
    }

    public static void AddIdentityDbConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.Ignore<IdentityUserToken<long>>();
        modelBuilder.Ignore<IdentityUserLogin<long>>();
        modelBuilder.Ignore<IdentityRoleClaim<long>>();
        modelBuilder.Ignore<IdentityUserClaim<long>>();
    }
}