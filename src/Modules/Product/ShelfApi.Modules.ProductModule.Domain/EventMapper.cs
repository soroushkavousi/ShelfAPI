using ShelfApi.Modules.ProductModule.Contracts.Events;

namespace ShelfApi.Modules.ProductModule.Domain;

public static class EventMapper
{
    public static ProductCreatedDomainEvent ToCreatedDomainEvent(this Product product)
        => new(() => product.Id, product.Name, product.Price.Value,
            product.Quantity, product.CreatedAt, product.ModifiedAt,
            product.IsDeleted, product.IsElasticsearchSynced);

    public static ProductUpdatedDomainEvent ToUpdatedDomainEvent(this Product product)
        => new(() => product.Id, product.Name, product.Price.Value,
            product.Quantity, product.CreatedAt, product.ModifiedAt,
            product.IsDeleted, product.IsElasticsearchSynced);

    public static ProductDeletedDomainEvent ToDeletedDomainEvent(this Product product)
        => new(() => product.Id, product.Name, product.Price.Value,
            product.Quantity, product.CreatedAt, product.ModifiedAt,
            product.IsDeleted, product.IsElasticsearchSynced);
}