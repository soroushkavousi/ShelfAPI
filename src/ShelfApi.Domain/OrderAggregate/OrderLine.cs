using ShelfApi.Domain.Common;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Domain.OrderAggregate;

public class OrderLine : BaseModel<ulong>
{
    private OrderLine() { }

    public OrderLine(ulong id, ulong orderId, Product product, int quantity) : base(id)
    {
        OrderId = orderId;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        TotalPrice = new(product.Price.Value * quantity);
    }

    public ulong OrderId { get; }
    public ulong ProductId { get; }
    public Product Product { get; }
    public int Quantity { get; }
    public Price TotalPrice { get; }
}