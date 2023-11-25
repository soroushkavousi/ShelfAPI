﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Infrastructure.Extensions;

namespace ShelfApi.Infrastructure.Data;

public class ConfigsConfiguration : IEntityTypeConfiguration<Configs>
{
    public void Configure(EntityTypeBuilder<Configs> builder)
    {
        builder.ConfigureKey(autoGenerated: false);

        builder.Property(x => x.EnvironmentName)
            .HasConversion<string>()
            .HasColumnType("varchar(60)")
            .HasColumnOrder(100)
            .IsRequired();

        builder.Property(x => x.Category)
            .HasConversion<string>()
            .HasColumnType("varchar(60)")
            .HasColumnOrder(101)
            .IsRequired();

        builder.Property(x => x.Value)
            .HasColumnType("nvarchar(max)")
            .HasDefaultValue("{}")
            .HasColumnOrder(102)
            .IsRequired();

        builder.ConfigureCreatedAt();
        builder.ConfigureModifiedAt();
    }
}