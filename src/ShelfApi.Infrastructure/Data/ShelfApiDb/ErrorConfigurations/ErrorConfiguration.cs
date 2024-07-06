using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.ErrorConfigurations;

public class ApiErrorConfiguration : IEntityTypeConfiguration<ApiError>
{
    public void Configure(EntityTypeBuilder<ApiError> builder)
    {
        builder.ConfigureKey(x => x.Code);

        builder.Property(x => x.Title)
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Message)
            .HasColumnOrder(102)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        builder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }
}