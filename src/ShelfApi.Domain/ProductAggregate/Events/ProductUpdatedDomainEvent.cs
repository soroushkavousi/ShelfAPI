namespace ShelfApi.Domain.ProductAggregate.Events;

public record ProductUpdatedDomainEvent : ProductDomainEvent
{
    private ProductUpdatedDomainEvent() { }
    public ProductUpdatedDomainEvent(Product product) : base(product) { }
}