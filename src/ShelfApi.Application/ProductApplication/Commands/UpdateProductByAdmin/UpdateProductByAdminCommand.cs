using ShelfApi.Application.ProductApplication.Models.Views.UserViews;

namespace ShelfApi.Application.ProductApplication.Commands.UpdateProductByAdmin;

public class UpdateProductByAdminCommand : IRequest<Result<ProductUserView>>
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}