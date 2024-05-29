using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.BaseDataConfigurations;

public class MainSettingsConfiguration : IEntityTypeConfiguration<MainSettings>
{
    public void Configure(EntityTypeBuilder<MainSettings> builder)
    {
        builder.ConfigureKey(x => x.Category);

        builder.Property(x => x.Category)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Data)
            .HasColumnType("nvarchar(max)")
            .HasDefaultValue("{}")
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}