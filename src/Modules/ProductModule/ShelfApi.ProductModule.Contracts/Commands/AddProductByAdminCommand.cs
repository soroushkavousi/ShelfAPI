using MediatR;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Contracts.Commands;

public class AddProductByAdminCommand : IRequest<Result<ProductUserView>>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}