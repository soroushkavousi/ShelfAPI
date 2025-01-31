using ShelfApi.Application.FinancialApplication.Dtos;

namespace ShelfApi.Application.FinancialApplication.Queries.GetCartPaymentPreview;

public class GetCartPaymentPreviewQuery : IRequest<Result<PaymentPreviewDto>>
{
    public required long UserId { get; init; }
}