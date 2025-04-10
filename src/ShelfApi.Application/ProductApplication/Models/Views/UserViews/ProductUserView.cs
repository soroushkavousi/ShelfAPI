using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Models.Views.UserViews;

public record ProductUserView
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ModifiedAt { get; init; }
}

public static class ProductUserViewExtensions
{
    public static ProductUserView ToUserView(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price.Value,
        Quantity = product.Quantity,
        CreatedAt = product.CreatedAt,
        ModifiedAt = product.ModifiedAt
    };
}