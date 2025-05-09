namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductCreatedDomainEvent : ProductDomainEvent
{
    private ProductCreatedDomainEvent() { }
    public ProductCreatedDomainEvent(Product product) : base(product) { }
}