namespace ShelfApi.FinancialModule.Application.ValueObjects;

public record FinancialSettings
{
    public decimal TaxPercentage { get; init; }
}