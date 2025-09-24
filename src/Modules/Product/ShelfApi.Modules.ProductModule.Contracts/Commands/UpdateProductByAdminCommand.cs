using MediatR;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.ProductModule.Contracts.Commands;

public class UpdateProductByAdminCommand : IRequest<Result<ProductUserView>>
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}