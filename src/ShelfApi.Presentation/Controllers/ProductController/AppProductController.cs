using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Application.ProductApplication.Queries.GetProduct;
using ShelfApi.Application.ProductApplication.Queries.ListProducts;
using ShelfApi.Presentation.Controllers.Common;

namespace ShelfApi.Presentation.Controllers.ProductController;

[Route("app/products")]
public class AppProductController(ISender sender) : AppBaseController(sender)
{
    [ProducesResponseType(typeof(Result<ProductUserView[]>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<Result<ProductUserView[]>>> ListProductsAsync(string name,
        decimal? minPrice, decimal? maxPrice)
    {
        Result<ProductUserView[]> result = await _sender.Send(new ListProductsQuery
        {
            Name = name,
            MinPrice = minPrice,
            MaxPrice = maxPrice
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