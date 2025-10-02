using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.CartModule.Domain;

namespace ShelfApi.CartModule.Infrastructure.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId);
        builder.Property(x => x.ProductId);
        builder.Property(x => x.UnitPrice);
        builder.Property(x => x.Quantity);
        
        builder.Property(x => x.LinePrice)
            .HasComputedColumnSql("[UnitPrice] * [Quantity]", true)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.ModifiedAt);
    }
}