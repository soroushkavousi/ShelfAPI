namespace ShelfApi.Modules.ProductModule.Application.Models.Dtos;

public static class ProductCacheKeys
{
    public static string GetProductKey(long id) => $"product:{id}";
}