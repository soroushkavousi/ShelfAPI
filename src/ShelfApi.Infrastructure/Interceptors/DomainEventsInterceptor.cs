using Bitiano.Shared.Tools.Serializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using ShelfApi.Domain.Common.Interfaces;
using ShelfApi.Domain.Common.Model;
using ShelfApi.Infrastructure.Models;

namespace ShelfApi.Infrastructure.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private static readonly HashSet<string> _contextIdsWithInterceptorTransaction = [];

    #region Asynchronous Operations

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        await BeginInterceptorTransactionIfNeededAsync(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static async Task BeginInterceptorTransactionIfNeededAsync(DbContext dbContext, CancellationToken ct)
    {
        if (dbContext.Database.CurrentTransaction != null)
            return;

        await dbContext.Database.BeginTransactionAsync(ct);
        _contextIdsWithInterceptorTransaction.Add(dbContext.ContextId.ToString());
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
        int result, CancellationToken cancellationToken = default)
    {
        int finalResult = await base.SavedChangesAsync(eventData, result, cancellationToken);

        if (eventData.Context == null)
            return finalResult;

        int addedDomainEventsCount = WriteOutboxMessages(eventData.Context);
        if (addedDomainEventsCount == 0)
            return finalResult;

        int finalResultWithAddedDomainEvents = await eventData.Context.SaveChangesAsync(cancellationToken);

        await CommitIfInterceptorTransactionUsedAsync(eventData.Context, cancellationToken);

        return finalResultWithAddedDomainEvents;
    }

    private static async Task CommitIfInterceptorTransactionUsedAsync(DbContext dbContext, CancellationToken ct)
    {
        string contextId = dbContext.ContextId.ToString();
        if (!_contextIdsWithInterceptorTransaction.Contains(contextId))
            return;

        IDbContextTransaction interceptorTransaction = dbContext.Database.CurrentTransaction;
        if (interceptorTransaction == null)
            throw new("No interceptor transaction found");

        await interceptorTransaction.CommitAsync(ct);
        await interceptorTransaction.DisposeAsync();
        _contextIdsWithInterceptorTransaction.Remove(contextId);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
            await RollbackIfInterceptorTransactionUsedAsync(eventData.Context, cancellationToken);

        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    private static async Task RollbackIfInterceptorTransactionUsedAsync(DbContext dbContext, CancellationToken ct)
    {
        string contextId = dbContext.ContextId.ToString();
        if (!_contextIdsWithInterceptorTransaction.Contains(contextId))
            return;

        IDbContextTransaction interceptorTransaction = dbContext.Database.CurrentTransaction;
        if (interceptorTransaction == null)
            throw new("No interceptor transaction found");

        await interceptorTransaction.RollbackAsync(ct);
        await interceptorTransaction.DisposeAsync();
        _contextIdsWithInterceptorTransaction.Remove(contextId);
    }

    #endregion Asynchronous Operations

    private static int WriteOutboxMessages(DbContext dbContext)
    {
        List<DomainModel> entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<DomainModel>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(entry => entry.Entity)
            .ToList();

        if (entitiesWithDomainEvents.Count == 0)
            return 0;

        DbSet<DomainEventOutboxMessage> outbox = dbContext.Set<DomainEventOutboxMessage>();

        foreach (DomainModel entity in entitiesWithDomainEvents)
        {
            IDomainEvent[] domainEvents = [.. entity.DomainEvents];
            entity.ClearDomainEvents();

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                domainEvent.ResetId();
                DomainEventOutboxMessage domainEventOutboxMessage = new(domainEvent.GetType().Name, domainEvent.ToJson());
                Console.WriteLine($"ChangeTracker 1: {dbContext.ChangeTracker.DebugView.ShortView}");
                outbox.Add(domainEventOutboxMessage);
                Console.WriteLine($"ChangeTracker 2: {dbContext.ChangeTracker.DebugView.ShortView}");
            }
        }

        return entitiesWithDomainEvents.Count;
    }

    #region Synchronous Operations

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context == null)
            return base.SavingChanges(eventData, result);

        BeginInterceptorTransactionIfNeeded(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private static void BeginInterceptorTransactionIfNeeded(DbContext dbContext)
    {
        if (dbContext.Database.CurrentTransaction != null)
            return;

        dbContext.Database.BeginTransaction();
        _contextIdsWithInterceptorTransaction.Add(dbContext.ContextId.ToString());
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        int finalResult = base.SavedChanges(eventData, result);

        if (eventData.Context == null)
            return finalResult;

        int addedDomainEventsCount = WriteOutboxMessages(eventData.Context);
        if (addedDomainEventsCount == 0)
            return finalResult;

        int finalResultWithAddedDomainEvents = eventData.Context.SaveChanges();

        CommitIfInterceptorTransactionUsed(eventData.Context);

        return finalResultWithAddedDomainEvents;
    }

    private static void CommitIfInterceptorTransactionUsed(DbContext dbContext)
    {
        string contextId = dbContext.ContextId.ToString();
        if (!_contextIdsWithInterceptorTransaction.Contains(contextId))
            return;

        IDbContextTransaction interceptorTransaction = dbContext.Database.CurrentTransaction;
        if (interceptorTransaction == null)
            throw new("No interceptor transaction found");

        interceptorTransaction.Commit();
        interceptorTransaction.Dispose();
        _contextIdsWithInterceptorTransaction.Remove(contextId);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (eventData.Context != null)
            RollbackIfInterceptorTransactionUsed(eventData.Context);

        base.SaveChangesFailed(eventData);
    }

    private static void RollbackIfInterceptorTransactionUsed(DbContext dbContext)
    {
        string contextId = dbContext.ContextId.ToString();
        if (!_contextIdsWithInterceptorTransaction.Contains(contextId))
            return;

        IDbContextTransaction interceptorTransaction = dbContext.Database.CurrentTransaction;
        if (interceptorTransaction == null)
            throw new("No interceptor transaction found");

        interceptorTransaction.Rollback();
        interceptorTransaction.Dispose();
        _contextIdsWithInterceptorTransaction.Remove(contextId);
    }

    #endregion Synchronous Operations
}