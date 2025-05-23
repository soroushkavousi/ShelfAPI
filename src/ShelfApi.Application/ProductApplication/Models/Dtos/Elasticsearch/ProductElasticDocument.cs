using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.ProductAggregate.Events;

namespace ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;

public record ProductElasticDocument
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public bool IsDeleted { get; init; }
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
        ModifiedAt = product.ModifiedAt,
        IsDeleted = product.IsDeleted
    };

    public static ProductElasticDocument ToElasticDocument(this ProductDomainEvent domainEvent) => new()
    {
        Id = domainEvent.Id,
        Name = domainEvent.Name,
        Price = domainEvent.Price,
        Quantity = domainEvent.Quantity,
        CreatedAt = domainEvent.CreatedAt,
        ModifiedAt = domainEvent.ModifiedAt,
        IsDeleted = domainEvent.IsDeleted
    };

    public static Product ToDomainModel(this ProductElasticDocument productElasticDocument) =>
        new(productElasticDocument.Id, productElasticDocument.Name,
            productElasticDocument.Price, productElasticDocument.Quantity,
            productElasticDocument.CreatedAt, productElasticDocument.ModifiedAt,
            productElasticDocument.IsDeleted, true);

    public static ProductUserView ToUserView(this ProductElasticDocument productElasticDocument) => new()
    {
        Id = productElasticDocument.Id,
        Name = productElasticDocument.Name,
        Price = productElasticDocument.Price,
        Quantity = productElasticDocument.Quantity,
        CreatedAt = productElasticDocument.CreatedAt,
        ModifiedAt = productElasticDocument.ModifiedAt
    };
}