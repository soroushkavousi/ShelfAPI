using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.OrderConfigurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.ConfigureKey(x => x.Id);

        builder.Property(x => x.OrderId)
            .HasColumnOrder(100);

        builder.Property(x => x.ProductId)
            .HasColumnOrder(101);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Quantity)
            .HasColumnOrder(102);

        builder.OwnsOne(x => x.TotalPrice, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("TotalPrice")
                .HasColumnType("DECIMAL(10,0)")
                .HasColumnOrder(103)
                .IsRequired();
        }).Navigation(x => x.TotalPrice).IsRequired();

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}