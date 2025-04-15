using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Dtos;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class DeleteProductCacheEventHandler(
    IFusionCache cache)
    : INotificationHandler<ProductCreatedEvent>,
        INotificationHandler<ProductUpdatedEvent>,
        INotificationHandler<ProductDeletedEvent>
{
    public async Task Handle(ProductCreatedEvent @event, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(@event.Product, cancellationToken);
    }

    public async Task Handle(ProductUpdatedEvent @event, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(@event.Product, cancellationToken);
    }

    public async Task Handle(ProductDeletedEvent @event, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(@event.Product, cancellationToken);
    }

    private async Task RemoveProductFromCacheAsync(ProductEventDto productEventDto, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(ProductCacheKeys.GetProductKey(productEventDto.Id));
    }
}