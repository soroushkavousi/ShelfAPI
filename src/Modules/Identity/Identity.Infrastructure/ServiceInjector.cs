using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Modules.Identity.Application;
using ShelfApi.Modules.Identity.Application.Services.TokenServicePack;
using ShelfApi.Modules.Identity.Infrastructure.Configurations;

namespace ShelfApi.Modules.Identity.Infrastructure;

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