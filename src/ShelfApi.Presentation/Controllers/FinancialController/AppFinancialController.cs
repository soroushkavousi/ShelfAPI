using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.CartModule.Contracts.Queries;
using ShelfApi.FinancialModule.Contracts.Commands;
using ShelfApi.Presentation.Controllers.Common;

namespace ShelfApi.Presentation.Controllers.FinancialController;

[Route("app/financial")]
public class AppFinancialController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<PaymentPreviewView>), StatusCodes.Status200OK)]
    [HttpGet("cart/buy/preview")]
    public async Task<ActionResult<Result<PaymentPreviewView>>> GetCartPaymentPreviewAsync()
    {
        Result<PaymentPreviewView> result = await _sender.Send(new GetCartPaymentPreviewQuery
        {
            UserId = UserId
        });

        return result;
    }
}