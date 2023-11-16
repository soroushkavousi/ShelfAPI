namespace ShelfApi.Application.ProductApplication;

public class ListProductsQueryHandler : ApiRequestHandler<ListProductsQuery, List<ProductDto>>
{
    public ListProductsQueryHandler()
    {

    }

    protected override async Task ValidateAsync(ListProductsQuery request, CancellationToken cancellationToken)
    {

    }

    protected override async Task<List<ProductDto>> OperateAsync(ListProductsQuery request, CancellationToken cancellationToken)
    {
        return new List<ProductDto>
        {
            new() { Id = 100, Name = "product-100" },
            new() { Id = 200, Name = "product-200" },
            new() { Id = 300, Name = "product-300" },
        };
    }
}

