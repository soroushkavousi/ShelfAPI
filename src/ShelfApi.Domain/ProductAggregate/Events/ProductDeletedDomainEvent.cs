namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductDeletedDomainEvent : ProductDomainEvent
{
    public ProductDeletedDomainEvent() { }
    public ProductDeletedDomainEvent(Product product) : base(product) { }
}