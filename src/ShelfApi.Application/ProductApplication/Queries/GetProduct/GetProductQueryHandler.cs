using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Models.Dtos;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.ProductApplication.Queries.GetProduct;

public class GetProductQueryHandler(IShelfApiDbContext dbContext, IFusionCache cache)
    : IRequestHandler<GetProductQuery, Result<ProductUserView>>
{
    public async Task<Result<ProductUserView>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ProductUserView product = await cache.GetOrSetAsync(
            ProductCacheKeys.GetProductKey(request.Id),
            _ => GetProductFromDatabaseAsync(request.Id, cancellationToken),
            options => options
                .SetDuration(TimeSpan.FromSeconds(2))
                .SetDistributedCacheDuration(TimeSpan.FromHours(1))
        );

        if (product is null)
            return ErrorCode.ItemNotFound;

        return product;
    }

    public async Task<ProductUserView> GetProductFromDatabaseAsync(long id, CancellationToken cancellationToken)
    {
        ProductUserView product = await dbContext.Products
            .Where(x => x.Id == id && !x.IsDeleted)
            .AsNoTracking()
            .Select(x => x.ToUserView())
            .FirstOrDefaultAsync(cancellationToken);

        return product;
    }
}