using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.ProductModule.Contracts.Events;

public abstract record ProductDomainEvent : IDomainEvent, IIntegrationEvent
{
    private readonly Func<long> _idFunc;

    protected ProductDomainEvent() { }

    protected ProductDomainEvent(Func<long> idFunc, string name, decimal price, int quantity,
        DateTime createdAt, DateTime? modifiedAt, bool isDeleted, bool isElasticsearchSynced)
    {
        _idFunc = idFunc;
        Name = name;
        Price = price;
        Quantity = quantity;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        IsDeleted = isDeleted;
        IsElasticsearchSynced = isElasticsearchSynced;
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
        Id = _idFunc();
    }
}