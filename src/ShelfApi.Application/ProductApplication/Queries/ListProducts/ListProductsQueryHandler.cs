using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ShelfApi.Application.ProductApplication;

public class ListProductsQueryHandler : ApiRequestHandler<ListProductsQuery, List<ProductDto>>
{
    private IShelfApiDbContext _dbContext;

    public ListProductsQueryHandler(IShelfApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task<List<ProductDto>> OperateAsync(ListProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductDto> products = await _dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        return products;
    }
}

