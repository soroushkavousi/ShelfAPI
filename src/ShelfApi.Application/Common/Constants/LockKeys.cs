namespace ShelfApi.Application.Common.Constants;

public static class LockKeys
{
    public static string GetAddProductToCartLockKey(long userId)
        => $"AddProductToCart_{userId}";
}