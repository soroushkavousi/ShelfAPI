using Microsoft.EntityFrameworkCore;
using ShelfApi.Modules.ProductModule.Infrastructure.Configurations;

namespace ShelfApi.Modules.ProductModule.Infrastructure;

public static class ServiceInjector
{
    public static void AddProductDbConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}