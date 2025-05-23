﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.ErrorConfigurations;

public class ApiErrorConfiguration : IEntityTypeConfiguration<ApiError>
{
    public void Configure(EntityTypeBuilder<ApiError> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .IsRequired();

        builder.Property(x => x.Message)
            .IsRequired();
    }
}