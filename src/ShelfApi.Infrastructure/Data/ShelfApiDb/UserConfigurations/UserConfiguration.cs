using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsAdmin);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.ModifiedAt);
    }
}