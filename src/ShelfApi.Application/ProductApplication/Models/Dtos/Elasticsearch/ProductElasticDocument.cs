using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.ProductAggregate;

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
    public static ProductElasticDocument ToElasticDocument(this ProductEventDto productEventDto) => new()
    {
        Id = productEventDto.Id,
        Name = productEventDto.Name,
        Price = productEventDto.Price,
        Quantity = productEventDto.Quantity,
        CreatedAt = productEventDto.CreatedAt,
        ModifiedAt = productEventDto.ModifiedAt,
        IsDeleted = productEventDto.IsDeleted
    };

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