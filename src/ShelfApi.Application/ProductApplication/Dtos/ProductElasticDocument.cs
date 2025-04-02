using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Dtos;

public record ProductElasticDocument
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
}

public static class ProductElasticDocumentExtensions
{
    public static ProductElasticDocument ToElasticDocument(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price.Value,
        Quantity = product.Quantity,
        CreatedAt = product.CreatedAt,
        ModifiedAt = product.ModifiedAt
    };

    public static Product ToDomain(this ProductElasticDocument productElasticDocument) =>
        new(productElasticDocument.Id, productElasticDocument.Name,
            productElasticDocument.Price, productElasticDocument.Quantity,
            productElasticDocument.CreatedAt, productElasticDocument.ModifiedAt);
}