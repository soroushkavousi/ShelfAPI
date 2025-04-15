namespace ShelfApi.Application.ProductApplication.Events;

public class ProductCreatedEvent : INotification
{
    public ProductEventDto Product { get; init; }
}