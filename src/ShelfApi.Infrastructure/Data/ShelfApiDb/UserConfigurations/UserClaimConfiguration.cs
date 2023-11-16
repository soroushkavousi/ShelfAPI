using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<ulong>> builder)
    {
        builder.ToTable("UserClaims");

        builder.ConfigureOrders();
        builder.ConfigureCreatedAt();
        builder.ConfigureModifiedAt();
    }
}