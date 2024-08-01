using ShelfApi.Domain.Common;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Domain.OrderAggregate;

public class OrderLine : BaseModel
{
    private OrderLine() { }

    public OrderLine(long orderId, Product product, int quantity) : base()
    {
        OrderId = orderId;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
        TotalPrice = Price.Create(product.Price.Value * quantity);
    }

    public long Id { get; }
    public long OrderId { get; }
    public long ProductId { get; }
    public Product Product { get; }
    public int Quantity { get; }
    public Price TotalPrice { get; }
}