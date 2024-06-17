using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;
using ShelfApi.Infrastructure.Common;
using ShelfApi.Infrastructure.Data.ShelfApiDb.BaseDataConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.OrderConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.ProductConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb;

public class ShelfApiDbContext : IdentityDbContext<User, Role, ulong>, IShelfApiDbContext
{
    public DbSet<ProjectSetting> ProjectSettings { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    private ShelfApiDbContext()
    { }

    public ShelfApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public ShelfApiDbContext(string connectionString)
        : base(CreateOptionsFromConnectionString(connectionString)) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().UseCollation("case-insensitive");

        configurationBuilder
            .Properties<DateTime>()
            .HavePrecision(2);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasCollation(Constants.CaseInsensitiveCollation, locale: "und-u-ks-level2", provider: "icu", deterministic: false);

        #region User Configs

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserClaimConfiguration());
        builder.ApplyConfiguration(new UserLoginConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());

        #endregion User Configs

        #region Settings

        builder.ApplyConfiguration(new ProjectSettingsConfiguration());

        #endregion Settings

        #region Product

        builder.ApplyConfiguration(new ProductConfiguration());

        #endregion Product

        #region Order

        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderLineConfiguration());

        #endregion Order
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
            e.Property(nameof(BaseModel.ModifiedAt)).CurrentValue = DateTime.UtcNow;
        });
    }

    public Task BulkInsertAsync<T>(IEnumerable<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class
        => DbContextBulkExtensions.BulkInsertAsync(this, entities, bulkConfig, progress, type, cancellationToken);

    public Task BulkInsertAsync<T>(IEnumerable<T> entities, Action<BulkConfig> bulkAction, Action<decimal> progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class
        => DbContextBulkExtensions.BulkInsertAsync(this, entities, bulkAction, progress, type, cancellationToken);

    public static DbContextOptions<ShelfApiDbContext> CreateOptionsFromConnectionString(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShelfApiDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return optionsBuilder.Options;
    }
}