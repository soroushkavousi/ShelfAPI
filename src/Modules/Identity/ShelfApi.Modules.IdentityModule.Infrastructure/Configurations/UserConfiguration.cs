using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Modules.IdentityModule.Domain;

namespace ShelfApi.Modules.IdentityModule.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.IsAdmin);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.ModifiedAt);
    }
}