using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShelfApi.Domain.FinancialAggregate;

namespace IPE.Sms.Infrastructure.Persistance.SmsDb.Configurations.FinancialConfigurations.Converters;

public class PriceConverter : ValueConverter<Price, decimal>
{
    public PriceConverter() : base(v => v.Value, v => new Price(v), new ConverterMappingHints(precision: 12, scale: 2))
    {
    }
}