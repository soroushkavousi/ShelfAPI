using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Presentation.Controllers.Common;
using ShelfApi.ProductModule.Contracts.Queries;
using ShelfApi.ProductModule.Contracts.Views;

namespace ShelfApi.Presentation.Controllers.ProductController;

[Route("app/products")]
public class AppProductController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<ProductUserView[]>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<Result<ProductUserView[]>>> ListProductsAsync(
        string name, decimal? minPrice, decimal? maxPrice,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        [FromQuery] bool sortDescending = true)
    {
        Result<ProductUserView[]> result = await _sender.Send(new ListProductsQuery
        {
            Name = name,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            PageNumber = pageNumber,
            PageSize = pageSize,
            SortDescending = sortDescending
        });

        return result;
    }

    [ProducesResponseType(typeof(Result<ProductUserView>), StatusCodes.Status200OK)]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Result<ProductUserView>>> GetProductAsync(long id)
    {
        Result<ProductUserView> result = await _sender.Send(new GetProductQuery
        {
            Id = id
        });

        return result;
    }
}