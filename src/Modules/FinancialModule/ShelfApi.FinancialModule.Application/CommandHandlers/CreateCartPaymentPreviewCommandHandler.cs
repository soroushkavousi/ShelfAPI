using MediatR;
using ShelfApi.FinancialModule.Application.QueryHandlers;
using ShelfApi.FinancialModule.Application.ValueObjects;
using ShelfApi.FinancialModule.Contracts.Commands;
using ShelfApi.FinancialModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.FinancialModule.Application.CommandHandlers;

public class CreateCartPaymentPreviewCommandHandler(IMediator mediator)
    : IRequestHandler<CreateCartPaymentPreviewCommand, Result<PaymentPreviewView>>
{
    public async Task<Result<PaymentPreviewView>> Handle(CreateCartPaymentPreviewCommand request, CancellationToken cancellationToken)
    {
        PaymentPreviewItemView[] paymentPreviewItems = request.PaymentLines
            .Select(x => new PaymentPreviewItemView
            {
                Name = x.Name,
                UnitPrice = x.UnitPrice.Value,
                Quantity = x.Quantity,
                TotalPrice = x.UnitPrice.Value * x.Quantity
            })
            .ToArray();

        decimal subTotal = paymentPreviewItems.Sum(x => x.TotalPrice);

        FinancialSettings financialSettings = await mediator.Send(new GetFinancialSettingsQuery());
        decimal taxPercentage = financialSettings.TaxPercentage;

        decimal tax = subTotal * taxPercentage / 100;
        Price finalPrice = subTotal + tax;

        PaymentPreviewView paymentPreview = new()
        {
            Items = paymentPreviewItems,
            SubTotal = subTotal,
            TaxPercentage = taxPercentage,
            Tax = tax,
            FinalPrice = finalPrice.Value
        };

        return paymentPreview;
    }
}