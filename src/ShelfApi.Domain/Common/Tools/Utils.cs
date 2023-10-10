namespace ShelfApi.Domain.Common;

public static class Utils
{
    public static string GenerateNewConcurrencyStamp()
        => Guid.NewGuid().ToString();
}
