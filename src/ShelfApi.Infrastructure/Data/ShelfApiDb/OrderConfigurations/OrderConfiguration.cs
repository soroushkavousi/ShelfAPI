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

        builder.OwnsOne(x => x.ListPrice, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("ListPrice")
                .HasColumnType("DECIMAL(10,0)")
                .HasColumnOrder(102)
                .IsRequired();
        }).Navigation(x => x.ListPrice).IsRequired();

        builder.OwnsOne(x => x.TaxPrice, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("TaxPrice")
                .HasColumnType("DECIMAL(10,0)")
                .HasColumnOrder(103)
                .IsRequired();
        }).Navigation(x => x.TaxPrice).IsRequired();

        builder.OwnsOne(x => x.NetPrice, nav =>
        {
            nav.Property(x => x.Value)
                .HasColumnName("NetPrice")
                .HasColumnType("DECIMAL(10,0)")
                .HasColumnOrder(104)
                .IsRequired();
        }).Navigation(x => x.NetPrice).IsRequired();

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}