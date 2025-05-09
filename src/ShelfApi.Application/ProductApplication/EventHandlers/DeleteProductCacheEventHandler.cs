// using MassTransit;
// using ShelfApi.Application.ProductApplication.Models.Dtos;
// using ShelfApi.Domain.ProductAggregate.Events;
// using ZiggyCreatures.Caching.Fusion;
//
// namespace ShelfApi.Application.ProductApplication.EventHandlers;
//
// public class DeleteProductCacheEventHandler(IFusionCache cache)
//     : IConsumer<ProductCreatedDomainEvent>,
//         IConsumer<ProductUpdatedDomainEvent>,
//         IConsumer<ProductDeletedDomainEvent>
// {
//     public async Task Consume(ConsumeContext<ProductCreatedDomainEvent> context)
//     {
//         await RemoveProductFromCacheAsync(context.Message.Product.Id, context.CancellationToken);
//     }
//
//     public async Task Consume(ConsumeContext<ProductUpdatedDomainEvent> context)
//     {
//         await RemoveProductFromCacheAsync(context.Message.Product.Id, context.CancellationToken);
//     }
//
//     public async Task Consume(ConsumeContext<ProductDeletedDomainEvent> context)
//     {
//         await RemoveProductFromCacheAsync(context.Message.Product.Id, context.CancellationToken);
//     }
//
//     private async Task RemoveProductFromCacheAsync(long productId, CancellationToken cancellationToken)
//     {
//         await cache.RemoveAsync(ProductCacheKeys.GetProductKey(productId));
//     }
// }

