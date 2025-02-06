using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.FinancialApplication.Dtos;
using ShelfApi.Application.FinancialApplication.Queries.GetFinancialSettings;
using ShelfApi.Domain.FinancialAggregate;

namespace ShelfApi.Application.FinancialApplication.Queries.GetCartPaymentPreview;

public class GetCartPaymentPreviewQueryHandler(IShelfApiDbContext dbContext, IMediator mediator)
    : IRequestHandler<GetCartPaymentPreviewQuery, Result<PaymentPreviewDto>>
{
    public async Task<Result<PaymentPreviewDto>> Handle(GetCartPaymentPreviewQuery request, CancellationToken cancellationToken)
    {
        PaymentPreviewItemDto[] paymentPreviewItems = await dbContext.CartItems
            .Include(x => x.Product)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new PaymentPreviewItemDto
            {
                ProductId = x.ProductId,
                Name = x.Product.Name,
                UnitPrice = x.Product.Price.Value,
                Quantity = x.Quantity,
                TotalPrice = x.Product.Price.Value * x.Quantity
            })
            .ToArrayAsync(cancellationToken);

        FinancialSettings financialSettings = await mediator.Send(new GetFinancialSettingsQuery());
        PaymentPreviewDto paymentPreview = new(paymentPreviewItems, financialSettings.TaxPercentage);

        return paymentPreview;
    }
}