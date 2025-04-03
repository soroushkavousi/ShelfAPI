using ShelfApi.Application.ProductApplication.Models.Views.UserViews;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommand : IRequest<Result<ProductUserView>>
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}