namespace ShelfApi.Application.ProductApplication.Events;

public class ProductDeletedEvent : INotification
{
    public ProductEventDto Product { get; init; }
}