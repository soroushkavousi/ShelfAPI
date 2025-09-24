using System.Linq.Expressions;
using ShelfApi.Modules.ProductModule.Contracts.Events;
using ShelfApi.Modules.ProductModule.Domain;

namespace ShelfApi.Modules.ProductModule.Application.Models.Dtos.Elasticsearch;

public record ProductElasticDocument
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public bool IsDeleted { get; init; }

    public static Expression<Func<Product, ProductElasticDocument>> FromProductExpr =>
        product => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price.Value,
            Quantity = product.Quantity,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt
        };

    public static Expression<Func<ProductDomainEvent, ProductElasticDocument>> FromProductDomainEventExpr =>
        productDomainEvent => new()
        {
            Id = productDomainEvent.Id,
            Name = productDomainEvent.Name,
            Price = productDomainEvent.Price,
            Quantity = productDomainEvent.Quantity,
            CreatedAt = productDomainEvent.CreatedAt,
            ModifiedAt = productDomainEvent.ModifiedAt
        };
}

public static class ProductElasticDocumentExtensions
{
    private static readonly Func<Product, ProductElasticDocument> _fromProduct
        = ProductElasticDocument.FromProductExpr.Compile();

    private static readonly Func<ProductDomainEvent, ProductElasticDocument> _fromProductDomainEvent
        = ProductElasticDocument.FromProductDomainEventExpr.Compile();

    public static ProductElasticDocument ToElasticDocument(this Product product)
        => _fromProduct(product);

    public static ProductElasticDocument ToElasticDocument(this ProductDomainEvent productDomainEvent)
        => _fromProductDomainEvent(productDomainEvent);
}