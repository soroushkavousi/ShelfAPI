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

        builder.OwnsOne(x => x.Price, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("Price")
                .HasColumnType("DECIMAL(10,0)")
                .HasColumnOrder(101)
                .IsRequired();
        }).Navigation(x => x.Price).IsRequired();

        builder.Property(x => x.Quantity)
            .HasColumnOrder(102);

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}