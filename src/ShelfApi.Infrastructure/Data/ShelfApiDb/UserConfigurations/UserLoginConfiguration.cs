using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<ulong>> builder)
    {
        builder.ToTable("UserLogins");

        builder.SetOrderForAllProperties();

        builder.Property(x => x.UserId)
            .HasColumnOrder(1);

        builder.Property<DateTime>(nameof(BaseModel.CreatedAt))
            .ConfigureCreatedAt();

        builder.Property<DateTime?>(nameof(BaseModel.ModifiedAt))
            .ConfigureModifiedAt();
    }
}