namespace ShelfApi.Domain.FinancialAggregate;

public record FinancialSettings
{
    public decimal TaxPercentage { get; init; }
}