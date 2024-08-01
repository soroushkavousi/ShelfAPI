using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Infrastructure.Extensions;

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

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}