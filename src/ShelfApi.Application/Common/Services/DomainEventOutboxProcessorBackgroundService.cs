using System.Diagnostics;
using Bitiano.Shared.Services.BackgroundServices;
using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.Common.Commands;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Models;

namespace ShelfApi.Application.Common.Services;

public class DomainEventOutboxProcessorBackgroundService(ILogger<DomainEventOutboxProcessorBackgroundService> logger,
    IServiceScopeFactory serviceScopeFactory) : PeriodicBackgroundService(logger, serviceScopeFactory)
{
    protected override TimeSpan Interval { get; } = TimeSpan.FromSeconds(2);
    private const int _batchSize = 100;
    private const int _maxRetryCount = 3;
    private const int _chunkSize = 10;

    protected override async Task ExecuteJobAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        IShelfApiDbContext dbContext = scope.ServiceProvider.GetRequiredService<IShelfApiDbContext>();
        IScopeService scopeService = scope.ServiceProvider.GetRequiredService<IScopeService>();

        Stopwatch stopwatch = Stopwatch.StartNew();

        DomainEventOutboxMessage[] unprocessedMessages = await dbContext.DomainEventOutboxMessages
            .Where(m => m.ProcessedOnUtc == null && m.RetryCount < _maxRetryCount)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(_batchSize)
            .ToArrayAsync(cancellationToken);

        if (unprocessedMessages.Length == 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            return;
        }

        logger.LogInformation("Processing {Count} outbox messages", unprocessedMessages.Length);

        long queryTime = stopwatch.ElapsedMilliseconds;
        stopwatch.Restart();

        int processedCount = 0;
        int chunkCount = 0;

        foreach (DomainEventOutboxMessage[] messageChunk in unprocessedMessages.Chunk(_chunkSize))
        {
            chunkCount++;
            List<Task<bool>> publishTasks = [];
            publishTasks.AddRange(messageChunk.Select(message => scopeService.Run(new PublishDomainEventCommand
            {
                OutboxMessage = message
            })));

            bool[] results = await Task.WhenAll(publishTasks);
            processedCount += results.Count(r => r);

            logger.LogDebug("Processed chunk {ChunkNumber} with {SuccessCount} successful events out of {TotalCount}",
                chunkCount, results.Count(r => r), messageChunk.Length);
        }

        long publishTime = stopwatch.ElapsedMilliseconds;
        stopwatch.Stop();

        logger.LogInformation(
            "Processed {SuccessCount}/{TotalCount} messages in {TotalTime}ms (Query: {QueryTime}ms, Publish: {PublishTime}ms)",
            processedCount, unprocessedMessages.Length, queryTime + publishTime, queryTime, publishTime);
    }
}