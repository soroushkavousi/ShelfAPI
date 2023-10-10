using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Infrastructure.Data;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<ulong>> builder)
    {
        builder.ToTable("UserRoles");
    }
}
