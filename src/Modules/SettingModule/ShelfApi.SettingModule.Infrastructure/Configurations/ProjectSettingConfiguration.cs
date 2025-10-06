using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.SettingModule.Domain;

namespace ShelfApi.SettingModule.Infrastructure.Configurations;

public class ProjectSettingConfiguration : IEntityTypeConfiguration<ProjectSetting>
{
    public void Configure(EntityTypeBuilder<ProjectSetting> builder)
    {
        builder.HasKey(x => x.Key);

        builder.Property(x => x.Value)
            .HasDefaultValue("{}")
            .IsRequired();
    }
}