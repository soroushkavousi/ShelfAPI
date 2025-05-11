namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductUpdatedDomainEvent : ProductDomainEvent
{
    public ProductUpdatedDomainEvent() { }
    public ProductUpdatedDomainEvent(Product product) : base(product) { }
}