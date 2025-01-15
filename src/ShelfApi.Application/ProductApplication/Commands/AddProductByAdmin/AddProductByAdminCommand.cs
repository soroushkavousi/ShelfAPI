using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommand : IRequest<Result<ProductDto>>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}