using ShelfApi.Application.ProductApplication.Models.Dtos;
using ShelfApi.Domain.ProductAggregate.Events;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class DeleteProductCacheEventHandler(IFusionCache cache)
    : INotificationHandler<ProductCreatedDomainEvent>,
        INotificationHandler<ProductUpdatedDomainEvent>,
        INotificationHandler<ProductDeletedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(domainEvent, cancellationToken);
    }

    public async Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(domainEvent, cancellationToken);
    }

    public async Task Handle(ProductDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await RemoveProductFromCacheAsync(domainEvent, cancellationToken);
    }

    private async Task RemoveProductFromCacheAsync(ProductDomainEvent productDomainEvent, CancellationToken cancellationToken)
    {
        await cache.RemoveAsync(ProductCacheKeys.GetProductKey(productDomainEvent.Id));
    }
}