using MediatR;
using ShelfApi.FinancialModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.FinancialModule.Application.Commands;

public class CreatePaymentPreviewCommand : IRequest<Result<PaymentPreviewView>>
{
    public required PaymentLine[] PaymentLines { get; init; }
}

public record PaymentLine
{
    public required string Name { get; init; }
    public required Price UnitPrice { get; init; }
    public required int Quantity { get; init; }
}