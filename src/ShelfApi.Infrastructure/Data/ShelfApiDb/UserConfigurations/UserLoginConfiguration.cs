using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<ulong>> builder)
    {
        builder.ToTable("UserLogins");

        builder.SetOrderForAllProperties();

        builder.Property(x => x.UserId)
            .HasColumnOrder(1);

        builder.ConfigureCreatedAt();
        builder.ConfigureModifiedAt();
    }
}
