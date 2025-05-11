namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductCreatedDomainEvent : ProductDomainEvent
{
    public ProductCreatedDomainEvent() { }
    public ProductCreatedDomainEvent(Product product) : base(product) { }
}