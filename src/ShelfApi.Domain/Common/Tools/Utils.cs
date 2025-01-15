namespace ShelfApi.Domain.Common.Tools;

public static class Utils
{
    public static string GenerateNewConcurrencyStamp()
        => Guid.NewGuid().ToString();
}