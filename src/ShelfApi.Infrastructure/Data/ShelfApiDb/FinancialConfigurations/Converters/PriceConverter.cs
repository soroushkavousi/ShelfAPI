using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb.FinancialConfigurations.Converters;

public class PriceConverter : ValueConverter<Price, decimal>
{
    public PriceConverter() : base(v => v.Value, v => Price.TryCreate(v).Data ?? Price.Zero, new ConverterMappingHints(precision: 12, scale: 2))
    {
    }
}