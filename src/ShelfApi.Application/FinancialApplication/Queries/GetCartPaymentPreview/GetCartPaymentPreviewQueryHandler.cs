using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.FinancialApplication.Dtos;

namespace ShelfApi.Application.FinancialApplication.Queries.GetCartPaymentPreview;

public class GetCartPaymentPreviewQueryHandler(IShelfApiDbContext dbContext,
    IBaseDataService baseDataService)
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

        PaymentPreviewDto paymentPreview = new(paymentPreviewItems,
            baseDataService.FinancialSettings.TaxPercentage);

        return paymentPreview;
    }
}