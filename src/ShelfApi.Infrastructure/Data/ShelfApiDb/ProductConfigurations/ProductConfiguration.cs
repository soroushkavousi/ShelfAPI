using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Infrastructure.Data.ShelfApiDb.Common;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.ProductConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Quantity);

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}