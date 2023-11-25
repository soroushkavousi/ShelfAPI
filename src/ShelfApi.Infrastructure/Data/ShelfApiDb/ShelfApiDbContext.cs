using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common;
using ShelfApi.Domain.Common;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Infrastructure.Data;

public class ShelfApiDbContext : IdentityDbContext<User, Role, ulong>, IShelfApiDbContext
{
    public DbSet<Configs> Configs { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    private ShelfApiDbContext() { }

    public ShelfApiDbContext(DbContextOptions options) : base(options) { }

    public ShelfApiDbContext(string connectionString)
        : base(CreateOptionsFromConnectionString(connectionString)) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region User Configs

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());

        #endregion

        #region Configuration

        builder.ApplyConfiguration(new ConfigsConfiguration());

        #endregion

        #region Product

        builder.ApplyConfiguration(new ProductConfiguration());

        #endregion

        #region Order

        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderLineConfiguration());

        #endregion
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        SetModifiedAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetModifiedAt()
    {
        var EditedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .ToList();

        EditedEntities.ForEach(e =>
        {
            e.Property(nameof(BaseModel<object>.ModifiedAt)).CurrentValue = DateTime.UtcNow;
        });
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