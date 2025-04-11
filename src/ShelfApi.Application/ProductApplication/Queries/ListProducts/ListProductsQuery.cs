using ShelfApi.Application.ProductApplication.Models.Views.UserViews;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public record ListProductsQuery : IRequest<Result<ProductUserView[]>>
{
    public string Name { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public bool SortDescending { get; init; } = true;
}