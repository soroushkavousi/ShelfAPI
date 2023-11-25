namespace ShelfApi.Application.ProductApplication;

public record ProductDto
{
    public ulong Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}
