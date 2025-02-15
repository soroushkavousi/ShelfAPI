﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.Common.Model;
using ShelfApi.Infrastructure.Common;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.Common;

public static class SqlConfigurationExtensions
{
    public static void ConfigureBaseModel<TProperty>(
        this EntityTypeBuilder<TProperty> propertyBuilder)
        where TProperty : BaseModel
    {
        propertyBuilder.Property(x => x.CreatedAt)
            .ConfigureCreatedAt();

        propertyBuilder.Property(x => x.ModifiedAt)
            .ConfigureModifiedAt();
    }

    public static PropertyBuilder ConfigureCreatedAt(this PropertyBuilder propertyBuilder)
        => propertyBuilder
            .IsRequired()
            .HasDefaultValueSql("now() at time zone 'utc'");

    public static PropertyBuilder<TProperty> ConfigureCreatedAt<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        ((PropertyBuilder)propertyBuilder)
            .ConfigureCreatedAt();

        return propertyBuilder;
    }

    public static PropertyBuilder ConfigureModifiedAt(this PropertyBuilder propertyBuilder)
        => propertyBuilder
            .IsRequired(false);

    public static PropertyBuilder<TProperty> ConfigureModifiedAt<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        ((PropertyBuilder)propertyBuilder)
            .ConfigureModifiedAt();

        return propertyBuilder;
    }

    public static PropertyBuilder<TProperty> CaseSensitive<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        propertyBuilder.UseCollation(Constants.CaseSensitiveCollation);
        return propertyBuilder;
    }

    public static PropertyBuilder<TProperty> CaseInsensitive<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        propertyBuilder.UseCollation(Constants.CaseInsensitiveCollation);
        return propertyBuilder;
    }
}