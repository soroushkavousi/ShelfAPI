// using Bitiano.Shared.Services.Elasticsearch;
// using MassTransit;
// using Microsoft.EntityFrameworkCore;
// using ShelfApi.Application.Common.Data;
// using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;
// using ShelfApi.Domain.ProductAggregate.Events;
//
// namespace ShelfApi.Application.ProductApplication.EventHandlers;
//
// public class IndexProductElasticDocumentEventHandler(
//     IElasticsearchService<ProductElasticDocument> productElasticsearchService,
//     IShelfApiDbContext dbContext)
//     : IConsumer<ProductCreatedDomainEvent>,
//         IConsumer<ProductUpdatedDomainEvent>,
//         IConsumer<ProductDeletedDomainEvent>
// {
//     public async Task Consume(ConsumeContext<ProductCreatedDomainEvent> context)
//         => await AddOrUpdateInElasticsearchAsync(context.Message.Product, context.CancellationToken);
//
//     public async Task Consume(ConsumeContext<ProductUpdatedDomainEvent> context)
//         => await AddOrUpdateInElasticsearchAsync(context.Message.Product, context.CancellationToken);
//
//     public async Task Consume(ConsumeContext<ProductDeletedDomainEvent> context)
//         => await AddOrUpdateInElasticsearchAsync(context.Message.Product, context.CancellationToken);
//
//     private async Task AddOrUpdateInElasticsearchAsync(ProductEventDto product, CancellationToken cancellationToken)
//     {
//         // ProductElasticDocument document = product.ToElasticDocument();
//         // await productElasticsearchService.AddOrUpdateAsync(document);
//         //
//         // await dbContext.Products
//         //     .Where(p => p.Id == document.Id)
//         //     .ExecuteUpdateAsync(p => p.SetProperty(p => p.IsElasticsearchSynced, true), cancellationToken);
//     }
// }

