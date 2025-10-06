using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.CartModule.Infrastructure.Configurations;

namespace ShelfApi.CartModule.Infrastructure;

public static class ServiceInjector
{
    public static void AddCartModule(this IServiceCollection services)
    {
    }

    public static void AddCartDbConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
    }
}