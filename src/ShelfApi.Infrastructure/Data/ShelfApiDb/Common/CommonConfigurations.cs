using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Infrastructure.Models;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.Common;

public static class SqlConfigurationExtensions
{
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