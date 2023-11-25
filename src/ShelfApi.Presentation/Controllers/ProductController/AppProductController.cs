using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.Common;
using ShelfApi.Application.ProductApplication;

namespace ShelfApi.Presentation.Controllers;

[Route("app/products")]
public class AppProductController : AppBaseController
{
    public AppProductController(ISender sender) : base(sender) { }

    [ProducesResponseType(typeof(ResultDto<List<ProductDto>>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<ResultDto<List<ProductDto>>>> ListProductsAsync()
    {
        var result = await _sender.Send(new ListProductsQuery
        {

        });

        return result;
    }
}
