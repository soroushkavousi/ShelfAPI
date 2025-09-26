using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Models.Dtos.Elasticsearch;
using ShelfApi.Shared.Common.Tools;
using ShelfApi.Shared.Elasticsearch;

namespace ShelfApi.ProductModule.Application.Services;

public class SyncProductElasticDocumentBackgroundService(ILogger<SyncProductElasticDocumentBackgroundService> logger,
    IServiceScopeFactory serviceScopeFactory) : PeriodicBackgroundService(logger, serviceScopeFactory)
{
    protected override TimeSpan Interval { get; } = TimeSpan.FromMinutes(1);
    private const int _chunkSize = 1000;

    protected override async Task ExecuteJobAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        IProductDbContext shelfApiDbContext = scope.ServiceProvider.GetRequiredService<IProductDbContext>();
        IElasticsearchService<ProductElasticDocument> productElasticsearchService = scope.ServiceProvider
            .GetRequiredService<IElasticsearchService<ProductElasticDocument>>();

        DateTime searchEndCreatedAt = DateTime.UtcNow.AddMinutes(-1);
        ProductElasticDocument[] productElasticDocuments = await shelfApiDbContext.Products
            .IgnoreQueryFilters()
            .Where(x => !x.IsElasticsearchSynced && x.CreatedAt <= searchEndCreatedAt)
            .Select(ProductElasticDocument.FromProductExpr)
            .ToArrayAsync();

        foreach (ProductElasticDocument[] chunk in productElasticDocuments.Chunk(_chunkSize))
        {
            (ElasticsearchErrorCode? errorCode, bool success) = await productElasticsearchService.BulkAddOrUpdateAsync(chunk);

            if (errorCode.HasValue || !success)
            {
                logger.LogWarning("Failed to sync products to elasticsearch" +
                    " - errorCode: {ErrorCode} - success: {Success}",
                    errorCode, success);
                return;
            }

            long[] chunkIds = chunk.Select(x => x.Id).ToArray();

            await shelfApiDbContext.Products
                .IgnoreQueryFilters()
                .Where(x => chunkIds.Contains(x.Id))
                .ExecuteUpdateAsync(setter => setter.SetProperty(x => x.IsElasticsearchSynced, true));
        }
    }
}