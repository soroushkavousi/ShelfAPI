namespace ShelfApi.Application.ProductApplication;

public record ProductDto
{
    public ulong Id { get; init; }
    public string Name { get; init; }
}
