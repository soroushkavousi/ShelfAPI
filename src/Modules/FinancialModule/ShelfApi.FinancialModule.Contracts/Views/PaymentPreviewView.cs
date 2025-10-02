namespace ShelfApi.FinancialModule.Contracts.Views;

public class PaymentPreviewView
{
    public PaymentPreviewItemView[] Items { get; init; } = [];
    public decimal SubTotal { get; init; }
    public decimal TaxPercentage { get; init; }
    public decimal Tax { get; init; }
    public decimal FinalPrice { get; init; }
}

public record PaymentPreviewItemView
{
    public required string Name { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int Quantity { get; init; }
    public required decimal TotalPrice { get; init; }
}