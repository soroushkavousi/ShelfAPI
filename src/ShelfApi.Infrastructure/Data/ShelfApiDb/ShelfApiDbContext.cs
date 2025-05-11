using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Models;
using ShelfApi.Domain.CartDomain;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.SettingDomain;
using ShelfApi.Domain.UserAggregate;
using ShelfApi.Infrastructure.Data.ShelfApiDb.CartConfiguration;
using ShelfApi.Infrastructure.Data.ShelfApiDb.ErrorConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.FinancialConfigurations.Converters;
using ShelfApi.Infrastructure.Data.ShelfApiDb.ProductConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.SettingConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.UserConfigurations;
using ShelfApi.Infrastructure.Interceptors;
using ShelfApi.Infrastructure.Models;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb;

public class ShelfApiDbContext : IdentityDbContext<User, Role, long>, IShelfApiDbContext
{
    private static readonly DomainEventInterceptor _domainEventInterceptor = new();

    public DbSet<DomainEventOutboxMessage> DomainEventOutboxMessages { get; set; }

    public DbSet<ProjectSetting> ProjectSettings { get; set; }

    public DbSet<ApiError> ApiErrors { get; set; }

    public DbSet<Product> Products { get; set; }

    #region Cart

    public DbSet<CartItem> CartItems { get; set; }

    #endregion Cart

    private ShelfApiDbContext() { }

    public ShelfApiDbContext(DbContextOptions options) : base(options) { }

    public ShelfApiDbContext(string connectionString)
        : base(CreateOptionsFromConnectionString(connectionString))
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<string>()
            .UseCollation(Constants.CaseInsensitiveCollation);

        configurationBuilder
            .Properties<Price>()
            .HaveConversion<PriceConverter>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasCollation(Constants.CaseInsensitiveCollation, locale: "und-u-ks-level2", provider: "icu", deterministic: false);

        builder.ApplyConfiguration(new DomainEventOutboxMessageConfiguration());

        #region User

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.Ignore<IdentityUserToken<long>>();
        builder.Ignore<IdentityUserLogin<long>>();
        builder.Ignore<IdentityRoleClaim<long>>();
        builder.Ignore<IdentityUserClaim<long>>();

        #endregion User

        #region Error

        builder.ApplyConfiguration(new ApiErrorConfiguration());

        #endregion Error

        #region Settings

        builder.ApplyConfiguration(new ProjectSettingsConfiguration());

        #endregion Settings

        #region Product

        builder.ApplyConfiguration(new ProductConfiguration());

        #endregion Product

        #region Cart

        builder.ApplyConfiguration(new CartItemConfiguration());

        #endregion Cart
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(_domainEventInterceptor);

    public Task BulkInsertAsync<T>(IEnumerable<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class
        => DbContextBulkExtensions.BulkInsertAsync(this, entities, bulkConfig, progress, type, cancellationToken);

    public Task BulkInsertAsync<T>(IEnumerable<T> entities, Action<BulkConfig> bulkAction, Action<decimal> progress = null, Type type = null, CancellationToken cancellationToken = default) where T : class
        => DbContextBulkExtensions.BulkInsertAsync(this, entities, bulkAction, progress, type, cancellationToken);

    public static DbContextOptions<ShelfApiDbContext> CreateOptionsFromConnectionString(string connectionString)
    {
        DbContextOptionsBuilder<ShelfApiDbContext> optionsBuilder = new DbContextOptionsBuilder<ShelfApiDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return optionsBuilder.Options;
    }
}