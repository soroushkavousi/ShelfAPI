using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common.Model;
using ShelfApi.Infrastructure.Data.ShelfApiDb.Common;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable("UserRoles");

        builder.Property<DateTime>(nameof(BaseModel.CreatedAt))
            .ConfigureCreatedAt();

        builder.Property<DateTime?>(nameof(BaseModel.ModifiedAt))
            .ConfigureModifiedAt();
    }
}