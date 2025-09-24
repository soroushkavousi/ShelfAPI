using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Modules.ProductModule.Application.Interfaces;
using ShelfApi.Modules.ProductModule.Application.Mappers;
using ShelfApi.Modules.ProductModule.Application.Models.Dtos;
using ShelfApi.Modules.ProductModule.Contracts.Queries;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Modules.ProductModule.Application.QueryHandlers;

public class GetProductQueryHandler(IProductDbContext dbContext, IFusionCache cache)
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
            .Select(ProductMapper.ProductToUserViewExpr)
            .FirstOrDefaultAsync(cancellationToken);

        return product;
    }
}