using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common;
using System.Reflection;

namespace ShelfApi.Infrastructure.Extensions;

public static class SqlConfigurationExtensions
{
    public static void AddBaseConfigurations<T>(this EntityTypeBuilder<T> builder,
        bool ignoreKey = false, bool ignoreCreatedAt = false, bool ignoreModifiedAt = false
        ) where T : class
    {
        var columns = builder.Metadata.ClrType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(x => x.Name)
            .ToList();

        int order = 100;
        foreach (var column in columns)
        {
            builder.Property(column).HasColumnOrder(order);
            order++;
        }

        if (!ignoreKey)
        {
            builder.HasKey(nameof(BaseModel.Id));

            builder.Property(nameof(BaseModel.Id))
                .ValueGeneratedNever()
                .HasColumnOrder(1);
        }

        if (!ignoreCreatedAt)
        {
            builder.Property<DateTime>(nameof(BaseModel.CreatedAt))
                .IsRequired(true)
                .HasColumnOrder(1000)
                .HasDefaultValueSql("SYSUTCDATETIME()");
        }

        if (!ignoreModifiedAt)
        {
            builder.Property<DateTime?>(nameof(BaseModel.ModifiedAt))
                .HasColumnOrder(1001)
                .IsRequired(false);
        }
    }
}
