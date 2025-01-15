using ShelfApi.Domain.Common.Model;
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

    public long Id { get; }
    public string Name { get; }
    public Price Price { get; }
    public int Quantity { get; }
}