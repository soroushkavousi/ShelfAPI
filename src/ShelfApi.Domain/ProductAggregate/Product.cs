using ShelfApi.Domain.Common;
using ShelfApi.Domain.FinancialAggregate;

namespace ShelfApi.Domain.ProductAggregate;

public class Product : BaseModel<ulong>
{
    private Product() { }

    public Product(ulong id, string name, Price price, int quantity) : base(id)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public string Name { get; }
    public Price Price { get; }
    public int Quantity { get; }
}