using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Infrastructure.Data;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.IsAdmin);

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.ModifiedAt);
    }
}
