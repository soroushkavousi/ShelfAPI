using ShelfApi.Domain.Common.Model;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Domain.CartDomain;

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
    public User User { get; }

    public long ProductId { get; }
    public Product Product { get; }

    public int Quantity { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    public void IncreaseQuantity(int quantity = 1)
    {
        Quantity += quantity;
        ModifiedAt = DateTime.UtcNow;
    }
}