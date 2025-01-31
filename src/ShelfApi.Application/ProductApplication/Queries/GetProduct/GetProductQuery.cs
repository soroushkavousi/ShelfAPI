using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Queries.GetProduct;

public class GetProductQuery : IRequest<Result<ProductDto>>
{
    public required long Id { get; init; }
}