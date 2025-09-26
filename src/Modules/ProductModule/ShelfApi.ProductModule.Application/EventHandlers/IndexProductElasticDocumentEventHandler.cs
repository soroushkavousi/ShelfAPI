using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Models.Dtos.Elasticsearch;
using ShelfApi.ProductModule.Contracts.Events;
using ShelfApi.Shared.Elasticsearch;

namespace ShelfApi.ProductModule.Application.EventHandlers;

public class IndexProductElasticDocumentEventHandler(
    IElasticsearchService<ProductElasticDocument> productElasticsearchService, IProductDbContext dbContext)
    : INotificationHandler<ProductCreatedDomainEvent>,
        INotificationHandler<ProductUpdatedDomainEvent>,
        INotificationHandler<ProductDeletedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent, cancellationToken);
    }

    public async Task Handle(ProductUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent, cancellationToken);
    }

    public async Task Handle(ProductDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await AddOrUpdateProductInElasticsearchAsync(domainEvent, cancellationToken);
    }

    private async Task AddOrUpdateProductInElasticsearchAsync(ProductDomainEvent productDomainEvent,
        CancellationToken cancellationToken)
    {
        ProductElasticDocument productElasticDocument = productDomainEvent.ToElasticDocument();

        await productElasticsearchService.AddOrUpdateAsync(productElasticDocument);

        await dbContext.Products
            .Where(p => p.Id == productElasticDocument.Id)
            .ExecuteUpdateAsync(p => p.SetProperty(p => p.IsElasticsearchSynced, true), cancellationToken);
    }
}