using ShelfApi.Domain.Common.Interfaces;

namespace ShelfApi.Domain.ProductAggregate.Events;

public abstract record ProductDomainEvent : IDomainEvent, IIntegrationEvent
{
    private readonly Product _product;

    protected ProductDomainEvent() { }

    protected ProductDomainEvent(Product product)
    {
        _product = product;
        Id = product.Id;
        Name = product.Name;
        Price = product.Price.Value;
        Quantity = product.Quantity;
        CreatedAt = product.CreatedAt;
        ModifiedAt = product.ModifiedAt;
        IsDeleted = product.IsDeleted;
        IsElasticsearchSynced = product.IsElasticsearchSynced;
    }

    public long Id { get; set; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public bool IsDeleted { get; init; }
    public bool IsElasticsearchSynced { get; init; }

    public void ResetId()
    {
        Id = _product.Id;
    }
}