using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data;

public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<ulong>> builder)
    {
        builder.ToTable("UserTokens");

        builder.ConfigureOrders();
        builder.ConfigureCreatedAt();
        builder.ConfigureModifiedAt();
    }
}
