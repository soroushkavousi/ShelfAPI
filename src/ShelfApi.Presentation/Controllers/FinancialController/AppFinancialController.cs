using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.FinancialApplication.Dtos;
using ShelfApi.Application.FinancialApplication.Queries.GetCartPaymentPreview;
using ShelfApi.Presentation.Controllers.Common;

namespace ShelfApi.Presentation.Controllers.FinancialController;

[Route("app")]
public class AppFinancialController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<PaymentPreviewDto>), StatusCodes.Status200OK)]
    [HttpGet("cart/buy/preview")]
    public async Task<ActionResult<Result<PaymentPreviewDto>>> GetCartPaymentPreviewAsync()
    {
        Result<PaymentPreviewDto> result = await _sender.Send(new GetCartPaymentPreviewQuery
        {
            UserId = UserId
        });

        return result;
    }
}