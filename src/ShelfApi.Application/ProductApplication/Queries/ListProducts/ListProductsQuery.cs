using ShelfApi.Application.ProductApplication.Models.Views.UserViews;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public class ListProductsQuery : IRequest<Result<ProductUserView[]>>
{
    public required string Name { get; init; }
    public required decimal? MinPrice { get; init; }
    public required decimal? MaxPrice { get; init; }
}