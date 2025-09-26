namespace ShelfApi.ProductModule.Contracts.Events;

public record ProductUpdatedDomainEvent : ProductDomainEvent
{
    public ProductUpdatedDomainEvent() { }

    public ProductUpdatedDomainEvent(Func<long> idFunc, string name, decimal price,
        int quantity, DateTime createdAt, DateTime? modifiedAt, bool isDeleted,
        bool isElasticsearchSynced) : base(idFunc, name, price, quantity, createdAt,
        modifiedAt, isDeleted, isElasticsearchSynced)
    {
    }
}