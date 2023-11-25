using ShelfApi.Domain.Common;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Domain.OrderAggregate;

public class Order : BaseModel<ulong>
{
    private Order() { }

    public Order(ulong id, ulong userId, List<OrderLine> orderLines) : base(id)
    {
        UserId = userId;
        State = OrderState.CREATED;
        SetOrderLines(orderLines);
    }

    private void SetOrderLines(List<OrderLine> orderLines)
    {
        Lines = orderLines;
        ListPrice = new(orderLines.Sum(x => x.TotalPrice.Value));
        TaxPrice = ListPrice.GetTaxPrice();
        NetPrice = ListPrice + TaxPrice;
    }

    public ulong UserId { get; }
    public User User { get; }
    public OrderState State { get; }
    public List<OrderLine> Lines { get; private set; }
    public Price ListPrice { get; private set; }
    public Price TaxPrice { get; private set; }
    public Price NetPrice { get; private set; }
}