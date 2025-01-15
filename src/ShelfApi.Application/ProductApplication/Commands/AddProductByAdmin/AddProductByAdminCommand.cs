using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommand : IRequest<Result<ProductDto>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}