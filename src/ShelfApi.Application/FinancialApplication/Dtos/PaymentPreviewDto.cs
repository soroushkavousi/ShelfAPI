namespace ShelfApi.Application.FinancialApplication.Dtos;

public record PaymentPreviewDto
{
    public PaymentPreviewDto() { }

    public PaymentPreviewDto(PaymentPreviewItemDto[] items, decimal taxPercentage)
    {
        Items = items.ToList();
        SubTotal = items.Sum(x => x.TotalPrice);
        TaxPercentage = (short)taxPercentage;
        Tax = SubTotal * taxPercentage / 100;
        FinalPrice = SubTotal + Tax;
    }

    public List<PaymentPreviewItemDto> Items { get; init; } = [];
    public decimal SubTotal { get; init; }
    public short TaxPercentage { get; init; }
    public decimal Tax { get; init; }
    public decimal FinalPrice { get; init; }
}

public record PaymentPreviewItemDto
{
    public required long ProductId { get; init; }
    public required string Name { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int Quantity { get; init; }
    public required decimal TotalPrice { get; init; }
}