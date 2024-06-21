using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.ProductConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ConfigureKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnOrder(100);

        builder.Property(x => x.Price)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnOrder(102);

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}