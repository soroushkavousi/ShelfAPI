using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.CartModule.Contracts.Commands;
using ShelfApi.Presentation.Controllers.Common;

namespace ShelfApi.Presentation.Controllers.CartController;

[Route("app")]
public class AppCartController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [HttpPost("products/{id:long}/cart")]
    public async Task<ActionResult<Result<bool>>> AddProductToCartAsync(long id)
    {
        Result<bool> result = await _sender.Send(new AddProductToCartCommand
        {
            UserId = UserId,
            ProductId = id
        });

        return result;
    }
}