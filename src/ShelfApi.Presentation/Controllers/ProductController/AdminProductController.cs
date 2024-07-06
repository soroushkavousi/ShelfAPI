using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.ProductApplication;

namespace ShelfApi.Presentation.Controllers;

[Route("admin/products")]
public class AdminProductController(ISender sender) : AdminBaseController(sender)
{
    [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<Result<ProductDto>>> AddProductByAdminAsync([FromBody] AddProductByAdminInput inputBody)
    {
        Result<ProductDto> result = await _sender.Send(new AddProductByAdminCommand
        {
            Name = inputBody.Name,
            Price = inputBody.Price,
            Quantity = inputBody.Quantity,
        });

        return result;
    }
}