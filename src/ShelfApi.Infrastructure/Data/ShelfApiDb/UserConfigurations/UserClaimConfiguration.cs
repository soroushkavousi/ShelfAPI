using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<ulong>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<ulong>> builder)
    {
        builder.ToTable("UserClaims");

        builder.SetOrderForAllProperties();

        builder.Property<DateTime>(nameof(BaseModel.CreatedAt))
            .ConfigureCreatedAt();

        builder.Property<DateTime?>(nameof(BaseModel.ModifiedAt))
            .ConfigureModifiedAt();
    }
}