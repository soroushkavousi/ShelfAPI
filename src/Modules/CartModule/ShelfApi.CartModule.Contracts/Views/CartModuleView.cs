using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Contracts.Views;

public record CartModuleView
{
    public long UserId { get; init; }
    public Price FinalPrice { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public CartItemModuleView[] CartItems { get; init; }
}

public record CartItemModuleView
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public long ProductId { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
}