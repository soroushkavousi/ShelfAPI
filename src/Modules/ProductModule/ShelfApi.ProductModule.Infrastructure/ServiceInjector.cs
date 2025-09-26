using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Infrastructure.Configurations;

namespace ShelfApi.ProductModule.Infrastructure;

public static class ServiceInjector
{
    public static void AddProductDbConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}