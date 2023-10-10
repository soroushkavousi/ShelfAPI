using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Infrastructure.Data;

public class ShelfApiDbContext : IdentityDbContext<User, Role, ulong>, IShelfApiDbContext
{
    private ShelfApiDbContext()
    { }

    public ShelfApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public ShelfApiDbContext(string connectionString)
        : base(CreateOptionsFromConnectionString(connectionString))
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
    }

    public async Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default) where T : class
    {
        await DbContextBulkExtensions.BulkInsertAsync(this, entities, bulkConfig, progress, cancellationToken: cancellationToken);
    }

    public async Task BulkDeleteAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default) where T : class
    {
        await DbContextBulkExtensions.BulkDeleteAsync(this, entities, bulkConfig, progress, cancellationToken: cancellationToken);
    }

    public async Task BulkUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default) where T : class
    {
        await DbContextBulkExtensions.BulkUpdateAsync(this, entities, bulkConfig, progress, cancellationToken: cancellationToken);
    }

    public static DbContextOptions<ShelfApiDbContext> CreateOptionsFromConnectionString(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShelfApiDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return optionsBuilder.Options;
    }
}