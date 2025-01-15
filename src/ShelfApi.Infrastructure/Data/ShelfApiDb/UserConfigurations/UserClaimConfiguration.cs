using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common.Model;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
    {
        builder.ToTable("UserClaims");

        builder.Property<DateTime>(nameof(BaseModel.CreatedAt))
            .ConfigureCreatedAt();

        builder.Property<DateTime?>(nameof(BaseModel.ModifiedAt))
            .ConfigureModifiedAt();
    }
}