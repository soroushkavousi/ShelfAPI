using MediatR;
using ShelfApi.CartModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Contracts.Queries;

public class GetUserCartByModuleQuery : IRequest<Result<CartModuleView>>
{
    public required long UserId { get; init; }
}