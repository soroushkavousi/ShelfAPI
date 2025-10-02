namespace ShelfApi.CartModule.Contracts.Views;

public record CartUserView
{
    public long UserId { get; init; }
    public long FinalPrice { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public CartItemUserView[] CartItems { get; init; }
}

public record CartItemUserView
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public long ProductId { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
}