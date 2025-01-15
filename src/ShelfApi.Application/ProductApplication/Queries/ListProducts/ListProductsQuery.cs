using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public class ListProductsQuery : IRequest<Result<List<ProductDto>>>
{
}