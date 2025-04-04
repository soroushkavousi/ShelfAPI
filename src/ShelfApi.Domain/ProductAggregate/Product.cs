﻿using ShelfApi.Domain.Common.Model;
using ShelfApi.Domain.FinancialAggregate;

namespace ShelfApi.Domain.ProductAggregate;

public class Product : BaseModel
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
    public string Name { get; }
    public Price Price { get; }
    public int Quantity { get; }
    public bool IsDeleted { get; }
    public bool IsElasticsearchSynced { get; }
}