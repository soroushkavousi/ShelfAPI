using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.Common.Models;
using ShelfApi.CartModule.Application.Interfaces;
using ShelfApi.CartModule.Domain;
using ShelfApi.CartModule.Infrastructure.Configurations;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.IdentityModule.Application.Interfaces;
using ShelfApi.IdentityModule.Domain;
using ShelfApi.IdentityModule.Infrastructure;
using ShelfApi.Infrastructure.Data.ShelfApiDb.ErrorConfigurations;
using ShelfApi.Infrastructure.Data.ShelfApiDb.FinancialConfigurations.Converters;
using ShelfApi.Infrastructure.Interceptors;
using ShelfApi.Infrastructure.Models;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Domain;
using ShelfApi.ProductModule.Infrastructure;
using ShelfApi.SettingModule.Application.Interfaces;
using ShelfApi.SettingModule.Domain;
using ShelfApi.SettingModule.Infrastructure;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Infrastructure.Data.ShelfApiDb;

public class ShelfApiDbContext : IdentityDbContext<User, Role, long>, IShelfApiDbContext,
    IIdentityDbContext, IProductDbContext, ICartDbContext, ISettingDbContext
{
    private static readonly DomainEventInterceptor _domainEventInterceptor = new();

    public DbSet<DomainEventOutboxMessage> DomainEventOutboxMessages { get; init; }

    public DbSet<ProjectSetting> ProjectSettings { get; init; }

    public DbSet<ApiError> ApiErrors { get; init; }

    public DbSet<Product> Products { get; init; }

    public DbSet<Cart> Carts { get; init; }
    public DbSet<CartItem> CartItems { get; init; }

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

        builder.AddIdentityDbConfigurations();
        builder.AddProductDbConfigurations();
        builder.AddSettingDbConfigurations();

        #region Error

        builder.ApplyConfiguration(new ApiErrorConfiguration());

        #endregion Error

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