using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Domain;

public class Cart: DomainModel
{
    private Cart() { }
    
    public Cart(int userId)
    {
        UserId = userId;
        Subtotal = Price.Zero;
    }

    public long UserId { get; }
    public Price Subtotal { get; private set; }
    public DateTime? ModifiedAt { get; }
    public ICollection<CartItem> Items { get; }
    
    public void AddItem(CartItem item)
    {
        Items.Add(item);
        Subtotal += item.LinePrice;
    }
}