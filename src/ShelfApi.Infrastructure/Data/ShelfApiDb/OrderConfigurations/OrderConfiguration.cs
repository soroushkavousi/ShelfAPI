using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.OrderConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ConfigureKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasColumnOrder(100);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.State)
            .HasColumnOrder(101)
            .IsRequired();

        builder.HasMany(x => x.Lines)
             .WithOne()
             .HasForeignKey(x => x.OrderId)
             .HasPrincipalKey(x => x.Id)
             .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.ListPrice)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.TaxPrice)
            .HasColumnOrder(103)
            .IsRequired();

        builder.Property(x => x.NetPrice)
            .HasColumnOrder(104)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}