﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common;
using System.Reflection;

namespace ShelfApi.Infrastructure.Extensions;

public static class SqlConfigurationExtensions
{
    public static void ConfigureOrders<T>(this EntityTypeBuilder<T> builder, int startOrderNumber = 100) where T : class
    {
        var columns = builder.Metadata.ClrType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(x => x.Name)
            .ToList();

        foreach (var column in columns)
        {
            builder.Property(column).HasColumnOrder(startOrderNumber);
            startOrderNumber++;
        }
    }

    public static void ConfigureAutoGeneratedKey<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.HasKey(nameof(BaseModel<object>.Id));

        builder.Property(nameof(BaseModel<object>.Id))
            .HasColumnOrder(1);
    }

    public static void ConfigureUlongKey<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.HasKey(nameof(BaseModel<ulong>.Id));

        builder.Property(nameof(BaseModel<ulong>.Id))
            .ValueGeneratedNever()
            .HasColumnOrder(1);
    }

    public static void ConfigureCreatedAt<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.Property<DateTime>(nameof(BaseModel<object>.CreatedAt))
            .IsRequired(true)
            .HasColumnOrder(1000)
            .HasDefaultValueSql("SYSUTCDATETIME()");
    }

    public static void ConfigureModifiedAt<T>(this EntityTypeBuilder<T> builder) where T : class
    {
        builder.Property<DateTime?>(nameof(BaseModel<object>.ModifiedAt))
            .HasColumnOrder(1001)
            .IsRequired(false);
    }
}