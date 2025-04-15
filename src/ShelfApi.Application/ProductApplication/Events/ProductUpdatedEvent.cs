namespace ShelfApi.Application.ProductApplication.Events;

public class ProductUpdatedEvent : INotification
{
    public ProductEventDto Product { get; init; }
}