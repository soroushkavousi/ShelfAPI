using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Infrastructure;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<ulong>> builder)
    {
        builder.ToTable("UserLogins");
    }
}
