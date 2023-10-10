using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace ShelfApi.Application.Common;

public interface IDbContext
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    DatabaseFacade Database { get; }

    ChangeTracker ChangeTracker { get; }

    void AttachRange(IEnumerable<object> entities);

    EntityEntry Entry([NotNullAttribute] object entity);

    EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;

    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

    Task BulkDeleteAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;

    Task BulkUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
}
