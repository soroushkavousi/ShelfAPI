using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Modules.IdentityModule.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable("UserRoles");
    }
}