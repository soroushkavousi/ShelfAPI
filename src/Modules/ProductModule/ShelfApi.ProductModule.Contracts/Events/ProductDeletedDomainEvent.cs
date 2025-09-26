namespace ShelfApi.ProductModule.Contracts.Events;

public record ProductDeletedDomainEvent : ProductDomainEvent
{
    public ProductDeletedDomainEvent() { }

    public ProductDeletedDomainEvent(Func<long> idFunc, string name, decimal price,
        int quantity, DateTime createdAt, DateTime? modifiedAt, bool isDeleted,
        bool isElasticsearchSynced) : base(idFunc, name, price, quantity, createdAt,
        modifiedAt, isDeleted, isElasticsearchSynced)
    {
    }
}