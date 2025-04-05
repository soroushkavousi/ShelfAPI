using Bitiano.Shared.Services.Elasticsearch;
using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class IndexProductElasticDocumentEventHandler(IElasticsearchService<ProductElasticDocument> productElasticsearchService)
    : INotificationHandler<ProductCreatedEvent>
{
    public async Task Handle(ProductCreatedEvent @event, CancellationToken cancellationToken)
    {
        await productElasticsearchService.AddOrUpdateAsync(@event.ToElasticDocument());
    }
}