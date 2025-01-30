using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Infrastructure.Data.ShelfApiDb.Common;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.BaseDataConfigurations;

public class ProjectSettingsConfiguration : IEntityTypeConfiguration<ProjectSetting>
{
    public void Configure(EntityTypeBuilder<ProjectSetting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Data)
            .HasDefaultValue("{}")
            .IsRequired();

        builder.ConfigureBaseModel();
    }
}