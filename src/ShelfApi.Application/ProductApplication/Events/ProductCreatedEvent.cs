using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Events;

public class ProductCreatedEvent : INotification
{
    public long Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ProductCreatedEventExtensions
{
    public static ProductCreatedEvent ToCreatedEvent(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price.Value,
        Quantity = product.Quantity,
        CreatedAt = product.CreatedAt
    };
}