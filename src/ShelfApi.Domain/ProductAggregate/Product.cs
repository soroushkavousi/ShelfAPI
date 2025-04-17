using ShelfApi.Domain.Common.Model;
using ShelfApi.Domain.FinancialAggregate;

namespace ShelfApi.Domain.ProductAggregate;

public class Product : DomainModel
{
    private Product() { }

    public Product(string name, Price price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public Product(long id, string name, Price price, int quantity, DateTime createdAt, DateTime? modifiedAt,
        bool isDeleted, bool isElasticsearchSynced)
        : this(name, price, quantity)
    {
        Id = id;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        IsDeleted = isDeleted;
        IsElasticsearchSynced = isElasticsearchSynced;
    }

    public long Id { get; }
    public string Name { get; private set; }
    public Price Price { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public bool IsElasticsearchSynced { get; private set; }

    public void Update(string name, Price price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        ModifiedAt = DateTime.UtcNow;
        IsElasticsearchSynced = false;
    }

    public void Delete()
    {
        IsDeleted = true;
        IsElasticsearchSynced = false;
        ModifiedAt = DateTime.UtcNow;
    }
}