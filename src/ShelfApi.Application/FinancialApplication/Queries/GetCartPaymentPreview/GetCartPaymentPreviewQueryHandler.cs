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
            .Select(PaymentPreviewItemDto.FromCartItemExpr)
            .ToArrayAsync(cancellationToken);

        FinancialSettings financialSettings = await mediator.Send(new GetFinancialSettingsQuery());
        PaymentPreviewDto paymentPreview = new(paymentPreviewItems, financialSettings.TaxPercentage);

        return paymentPreview;
    }
}