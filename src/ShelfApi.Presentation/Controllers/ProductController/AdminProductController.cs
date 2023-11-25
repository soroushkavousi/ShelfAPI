using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.Common;
using ShelfApi.Application.ProductApplication;

namespace ShelfApi.Presentation.Controllers;

[Route("admin/products")]
public class AdminProductController : AdminBaseController
{
    public AdminProductController(ISender sender) : base(sender) { }

    [ProducesResponseType(typeof(ResultDto<ProductDto>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<ResultDto<ProductDto>>> AddProductByAdminAsync([FromBody] AddProductByAdminInput inputBody)
    {
        var result = await _sender.Send(new AddProductByAdminCommand
        {
            Name = inputBody.Name,
            Price = new(inputBody.Price),
            Quantity = inputBody.Quantity,
        });

        return result;
    }
}
