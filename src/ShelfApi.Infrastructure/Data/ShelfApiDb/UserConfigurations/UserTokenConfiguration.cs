using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShelfApi.Infrastructure.Data;

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<ulong>> builder)
    {
        builder.ToTable("UserTokens");
    }
}
