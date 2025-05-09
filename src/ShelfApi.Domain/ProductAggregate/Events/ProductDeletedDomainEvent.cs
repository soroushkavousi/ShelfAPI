namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductDeletedDomainEvent : ProductDomainEvent
{
    private ProductDeletedDomainEvent() { }
    public ProductDeletedDomainEvent(Product product) : base(product) { }
}