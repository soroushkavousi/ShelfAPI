using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace ShelfApi.Application.Common;

public interface IDbContext
{
    int SaveChanges();

    void Dispose();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DatabaseFacade Database { get; }

    ChangeTracker ChangeTracker { get; }

    void AttachRange(IEnumerable<object> entities);

    EntityEntry Entry([NotNull] object entity);

    EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    Task BulkInsertAsync<T>(IEnumerable<T> entities, BulkConfig bulkConfig = null, Action<decimal>? progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class;

    Task BulkInsertAsync<T>(IEnumerable<T> entities, Action<BulkConfig> bulkAction, Action<decimal>? progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class;
}