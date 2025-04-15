namespace ShelfApi.Presentation.Controllers.ProductController.Inputs;

public class UpdateProductByAdminInput
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
}