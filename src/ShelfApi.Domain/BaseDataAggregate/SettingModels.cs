namespace ShelfApi.Domain.BaseDataAggregate;

public record FinancialSettings
{
    public decimal TaxPercentage { get; init; }
}