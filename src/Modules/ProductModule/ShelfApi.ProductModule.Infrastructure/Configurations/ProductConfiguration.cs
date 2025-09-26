using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.ProductModule.Domain;

namespace ShelfApi.ProductModule.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Quantity);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.ModifiedAt);
        builder.Property(x => x.IsDeleted);
        builder.Property(x => x.IsElasticsearchSynced);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}