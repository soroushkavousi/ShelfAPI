using Bitiano.Shared.Services.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.ProductAggregate.Events;

namespace ShelfApi.Application.ProductApplication.EventHandlers;

public class IndexProductElasticDocumentEventHandler(
    IElasticsearchService<ProductElasticDocument> productElasticsearchService,
    IShelfApiDbContext dbContext)
    : INotificationHandler<ProductCreatedDomainEvent>,
        INotificationHandler<ProductUpdatedDomainEvent>,
        INotificationHandler<ProductDeletedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent.Product, cancellationToken);
    }

    public async Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent.Product, cancellationToken);
    }

    public async Task Handle(ProductDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent.Product, cancellationToken);
    }

    private async Task AddOrUpdateProductInElasticsearchAsync(Product product, CancellationToken cancellationToken)
    {
        ProductElasticDocument productElasticDocument = product.ToElasticDocument();

        await productElasticsearchService.AddOrUpdateAsync(productElasticDocument);

        await dbContext.Products
            .Where(p => p.Id == productElasticDocument.Id)
            .ExecuteUpdateAsync(p => p
                    .SetProperty(p => p.IsElasticsearchSynced, true)
                , cancellationToken);
    }
}