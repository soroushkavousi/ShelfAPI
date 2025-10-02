using ShelfApi.IdentityModule.Domain;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Domain;

public class CartItem : DomainModel
{
    private CartItem() { }

    public CartItem(long id, long userId, long productId, int quantity = 1)
    {
        Id = id;
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
    }

    public long Id { get; }
    public long UserId { get; }
    public long ProductId { get; }
    public Price UnitPrice { get; }
    public int Quantity { get; private set; }
    public Price LinePrice { get => UnitPrice * Quantity; init => _ = value; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }
    

    public void IncreaseQuantity(int quantity = 1)
    {
        Quantity += quantity;
        ModifiedAt = DateTime.UtcNow;
    }
}