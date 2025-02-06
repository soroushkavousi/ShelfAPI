using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.ProductApplication.Queries.GetProduct;

public class GetProductQueryHandler(IShelfApiDbContext dbContext, IFusionCache cache)
    : IRequestHandler<GetProductQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ProductDto product = await cache.GetOrSetAsync(
            $"product:{request.Id}",
            _ => GetProductFromDatabaseAsync(request.Id, cancellationToken),
            options => options
                .SetDuration(TimeSpan.FromSeconds(2))
                .SetDistributedCacheDuration(TimeSpan.FromHours(1))
        );

        if (product is null)
            return ErrorCode.ItemNotFound;

        return product;
    }

    public async Task<ProductDto> GetProductFromDatabaseAsync(long id, CancellationToken cancellationToken)
    {
        ProductDto product = await dbContext.Products
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .FirstOrDefaultAsync(cancellationToken);

        return product;
    }
}