using MediatR;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Contracts.Commands;

public class DeleteProductByAdminCommand : IRequest<Result<bool>>
{
    public required long Id { get; init; }
}