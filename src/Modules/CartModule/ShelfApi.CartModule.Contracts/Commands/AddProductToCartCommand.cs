using MediatR;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Contracts.Commands;

public class AddProductToCartCommand : IRequest<Result<bool>>
{
    public required long UserId { get; init; }
    public required long ProductId { get; init; }
}