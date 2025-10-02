using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Domain;

public class Cart: DomainModel
{
    private Cart() { }
    
    public Cart(int userId)
    {
        UserId = userId;
        SubTotal = Price.Zero;
    }

    public long UserId { get; }
    public Price SubTotal { get; private set; }
    public DateTime? ModifiedAt { get; }
    public ICollection<CartItem> Items { get; }
    
    public void AddItem(CartItem item)
    {
        Items.Add(item);
        SubTotal += item.LinePrice;
    }
}