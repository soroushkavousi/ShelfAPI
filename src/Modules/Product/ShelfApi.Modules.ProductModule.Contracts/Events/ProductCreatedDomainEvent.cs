namespace ShelfApi.Modules.ProductModule.Contracts.Events;

public record ProductCreatedDomainEvent : ProductDomainEvent
{
    public ProductCreatedDomainEvent() { }

    public ProductCreatedDomainEvent(Func<long> idFunc, string name, decimal price,
        int quantity, DateTime createdAt, DateTime? modifiedAt, bool isDeleted,
        bool isElasticsearchSynced) : base(idFunc, name, price, quantity, createdAt,
        modifiedAt, isDeleted, isElasticsearchSynced)
    {
    }
}