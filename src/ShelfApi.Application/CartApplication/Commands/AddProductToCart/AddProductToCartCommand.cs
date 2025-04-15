namespace ShelfApi.Application.CartApplication.Commands.AddProductToCart;

public class AddProductToCartCommand : IRequest<Result<bool>>
{
    public required long UserId { get; init; }
    public required long ProductId { get; init; }
}