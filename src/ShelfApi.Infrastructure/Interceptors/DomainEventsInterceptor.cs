using Bitiano.Shared.Tools.Serializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using ShelfApi.Domain.Common.Interfaces;
using ShelfApi.Domain.Common.Model;
using ShelfApi.Infrastructure.Data.ShelfApiDb;
using ShelfApi.Infrastructure.Models;

namespace ShelfApi.Infrastructure.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private static HashSet<string> _transactionContexts = [];
    #region Asynchronous Operations

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await BeginLocalTransactionIfNeededAsync(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
        int result, CancellationToken cancellationToken = default)
    {
        string id = eventData.Context.ContextId.ToString();
        if (_transactionContexts.Contains(id))
        {
            Console.WriteLine("fucking contains 2");
        }
        WriteOutboxMessages(eventData.Context);
        int finalResult = await base.SavedChangesAsync(eventData, result, cancellationToken);
        await CommitIfLocalTransactionUsedAsync(eventData.Context, cancellationToken);
        return finalResult;
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        string id = eventData.Context.ContextId.ToString();
        if (_transactionContexts.Contains(id))
        {
            Console.WriteLine("fucking contains 2");
        }
        await base.SaveChangesFailedAsync(eventData, cancellationToken);
        await RollbackIfLocalTransactionUsedAsync(eventData.Context, cancellationToken);
    }

    private async Task BeginLocalTransactionIfNeededAsync(DbContext dbContext, CancellationToken ct)
    {
        if (dbContext == null) return;
        if (dbContext.Database.CurrentTransaction != null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;

        IDbContextTransaction localTransaction = await dbContext.Database.BeginTransactionAsync(ct);
        string id = dbContext.ContextId.ToString();
        transactionMarker.OutboxTransactionStarted = true;
        _transactionContexts.Add(id);
    }

    private async Task CommitIfLocalTransactionUsedAsync(DbContext dbContext, CancellationToken ct)
    {
        string id = dbContext.ContextId.ToString();
        if (_transactionContexts.Contains(id))
        {
            Console.WriteLine("fucking contains 2");
        }
        if (dbContext == null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;
        if (!transactionMarker.OutboxTransactionStarted) return;

        IDbContextTransaction localTransaction = dbContext.Database.CurrentTransaction;
        if (localTransaction != null)
        {
            await localTransaction.CommitAsync(ct);
            await localTransaction.DisposeAsync();
            transactionMarker.OutboxTransactionStarted = false;
        }
    }

    private async Task RollbackIfLocalTransactionUsedAsync(DbContext dbContext, CancellationToken ct)
    {
        if (dbContext == null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;
        if (!transactionMarker.OutboxTransactionStarted) return;

        IDbContextTransaction localTransaction = dbContext.Database.CurrentTransaction;
        if (localTransaction != null)
        {
            await localTransaction.RollbackAsync(ct);
            await localTransaction.DisposeAsync();
            transactionMarker.OutboxTransactionStarted = false;
        }
    }

    #endregion Asynchronous Operations

    private static void WriteOutboxMessages(DbContext dbContext)
    {
        if (dbContext == null) return;

        List<DomainModel> entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<DomainModel>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(entry => entry.Entity)
            .ToList();

        if (entitiesWithDomainEvents.Count == 0)
            return;

        DbSet<DomainEventOutboxMessage> outbox = dbContext.Set<DomainEventOutboxMessage>();

        foreach (DomainModel entity in entitiesWithDomainEvents)
        {
            IDomainEvent[] domainEvents = [.. entity.DomainEvents];
            entity.ClearDomainEvents();

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                domainEvent.ResetId();
                DomainEventOutboxMessage domainEventOutboxMessage = new(domainEvent.GetType().Name, domainEvent.ToJson());
                outbox.Add(domainEventOutboxMessage);
            }
        }
    }

    #region Synchronous Operations

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        BeginLocalTransactionIfNeeded(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        WriteOutboxMessages(eventData.Context);
        int finalResult = base.SavedChanges(eventData, result);
        CommitIfLocalTransactionUsed(eventData.Context);
        return finalResult;
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        base.SaveChangesFailed(eventData);
        RollbackIfLocalTransactionUsed(eventData.Context);
    }

    private void BeginLocalTransactionIfNeeded(DbContext dbContext)
    {
        if (dbContext == null) return;
        if (dbContext.Database.CurrentTransaction != null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;

        IDbContextTransaction localTransaction = dbContext.Database.BeginTransaction();
        transactionMarker.OutboxTransactionStarted = true;
    }

    private void CommitIfLocalTransactionUsed(DbContext dbContext)
    {
        if (dbContext == null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;
        if (!transactionMarker.OutboxTransactionStarted) return;

        IDbContextTransaction localTransaction = dbContext.Database.CurrentTransaction;
        if (localTransaction != null)
        {
            localTransaction.Commit();
            localTransaction.Dispose();
            transactionMarker.OutboxTransactionStarted = false;
        }
    }

    private void RollbackIfLocalTransactionUsed(DbContext dbContext)
    {
        if (dbContext == null) return;
        if (dbContext is not ITransactionMarker transactionMarker) return;
        if (!transactionMarker.OutboxTransactionStarted) return;

        IDbContextTransaction localTransaction = dbContext.Database.CurrentTransaction;
        if (localTransaction != null)
        {
            localTransaction.Rollback();
            localTransaction.Dispose();
            transactionMarker.OutboxTransactionStarted = false;
        }
    }

    #endregion Synchronous Operations
}