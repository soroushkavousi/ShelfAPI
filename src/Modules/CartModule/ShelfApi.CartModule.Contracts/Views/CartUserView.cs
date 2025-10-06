namespace ShelfApi.CartModule.Contracts.Views;

public record CartUserView
{
    public long UserId { get; init; }
    public decimal Subtotal { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public CartItemUserView[] Items { get; init; }
}

public record CartItemUserView
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public long ProductId { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal LinePrice { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
}