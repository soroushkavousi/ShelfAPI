using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.ProductApplication.Dtos;
using ShelfApi.Application.ProductApplication.Queries.ListProducts;
using ShelfApi.Presentation.Controllers.Common;

namespace ShelfApi.Presentation.Controllers.ProductController;

[Route("app/products")]
public class AppProductController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<List<ProductDto>>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<Result<List<ProductDto>>>> ListProductsAsync()
    {
        Result<List<ProductDto>> result = await _sender.Send(new ListProductsQuery());

        return result;
    }
}