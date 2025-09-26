using MediatR;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Contracts.Queries;

public class GetProductQuery : IRequest<Result<ProductUserView>>
{
    public required long Id { get; init; }
}