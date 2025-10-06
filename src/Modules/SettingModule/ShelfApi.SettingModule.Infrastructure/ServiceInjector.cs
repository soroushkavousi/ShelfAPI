using Microsoft.EntityFrameworkCore;
using ShelfApi.SettingModule.Infrastructure.Configurations;

namespace ShelfApi.SettingModule.Infrastructure;

public static class ServiceInjector
{
    public static void AddSettingDbConfigurations(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectSettingConfiguration());
    }
}