using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Mappers;
using ShelfApi.ProductModule.Application.Models.Dtos;
using ShelfApi.ProductModule.Contracts.Queries;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.ProductModule.Application.QueryHandlers;

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