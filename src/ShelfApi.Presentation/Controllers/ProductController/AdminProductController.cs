using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;
using ShelfApi.Application.ProductApplication.Dtos;
using ShelfApi.Presentation.Controllers.Common;
using ShelfApi.Presentation.Controllers.ProductController.Inputs;

namespace ShelfApi.Presentation.Controllers.ProductController;

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
            Quantity = inputBody.Quantity
        });

        return result;
    }
}