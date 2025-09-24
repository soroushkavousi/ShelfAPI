using MediatR;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.ProductModule.Contracts.Queries;

public class GetProductQuery : IRequest<Result<ProductUserView>>
{
    public required long Id { get; init; }
}