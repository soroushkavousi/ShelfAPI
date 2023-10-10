using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Infrastructure.Data;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<ulong>> builder)
    {
        builder.ToTable("RoleClaims");
    }
}
