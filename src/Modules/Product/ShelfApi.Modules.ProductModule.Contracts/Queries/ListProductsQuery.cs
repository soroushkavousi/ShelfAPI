using MediatR;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.ProductModule.Contracts.Queries;

public record ListProductsQuery : IRequest<Result<ProductUserView[]>>
{
    public string Name { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public bool SortDescending { get; init; } = true;
}