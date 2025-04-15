using Bitiano.Shared.Services.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class IndexProductElasticDocumentEventHandler(
    IElasticsearchService<ProductElasticDocument> productElasticsearchService,
    IShelfApiDbContext dbContext)
    : INotificationHandler<ProductCreatedEvent>,
        INotificationHandler<ProductUpdatedEvent>,
        INotificationHandler<ProductDeletedEvent>
{
    public async Task Handle(ProductCreatedEvent @event, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(@event.Product, cancellationToken);
    }

    public async Task Handle(ProductUpdatedEvent @event, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(@event.Product, cancellationToken);
    }

    public async Task Handle(ProductDeletedEvent @event, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(@event.Product, cancellationToken);
    }

    private async Task AddOrUpdateProductInElasticsearchAsync(ProductEventDto productEventDto, CancellationToken cancellationToken)
    {
        ProductElasticDocument productElasticDocument = productEventDto.ToElasticDocument();

        await productElasticsearchService.AddOrUpdateAsync(productElasticDocument);

        await dbContext.Products
            .Where(p => p.Id == productElasticDocument.Id)
            .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.IsElasticsearchSynced, true)
                , cancellationToken);
    }
}