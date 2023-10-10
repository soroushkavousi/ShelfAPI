using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Infrastructure.Data;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<ulong>> builder)
    {
        builder.ToTable("UserClaims");
    }
}
