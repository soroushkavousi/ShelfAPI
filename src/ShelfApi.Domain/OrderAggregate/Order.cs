using ShelfApi.Domain.Common;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Domain.OrderAggregate;

public class Order : BaseModel
{
    private Order() { }

    public Order(long userId, decimal taxPercentage, List<OrderLine> orderLines) : base()
    {
        UserId = userId;
        State = OrderState.CREATED;
        TaxPercentage = taxPercentage;
        SetOrderLines(orderLines);
    }

    private void SetOrderLines(List<OrderLine> orderLines)
    {
        Lines = orderLines;
        ListPrice = Price.Create(orderLines.Sum(x => x.TotalPrice.Value));
        OnOrderLineChanged();
    }

    private void OnOrderLineChanged()
    {
        TaxPrice = ListPrice.GetTax(TaxPercentage);
        NetPrice = ListPrice + TaxPrice;
    }

    public long Id { get; }
    public long UserId { get; }
    public User User { get; }
    public OrderState State { get; }
    public decimal TaxPercentage { get; }
    public List<OrderLine> Lines { get; private set; }
    public Price ListPrice { get; private set; }
    public Price TaxPrice { get; private set; }
    public Price NetPrice { get; private set; }
}