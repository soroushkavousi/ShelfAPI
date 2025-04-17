using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.SettingDomain;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.SettingConfigurations;

public class ProjectSettingsConfiguration : IEntityTypeConfiguration<ProjectSetting>
{
    public void Configure(EntityTypeBuilder<ProjectSetting> builder)
    {
        builder.HasKey(x => x.Key);

        builder.Property(x => x.Value)
            .HasDefaultValue("{}")
            .IsRequired();
    }
}