namespace ShelfApi.CartModule.Application.Constants;

public static class LockKeys
{
    public static string GetAddProductToCartLockKey(long userId)
        => $"AddProductToCart_{userId}";
}