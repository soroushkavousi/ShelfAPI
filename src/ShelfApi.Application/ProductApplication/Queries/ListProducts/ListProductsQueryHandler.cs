using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ShelfApi.Application.ProductApplication;

public class ListProductsQueryHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<ListProductsQuery, Result<List<ProductDto>>>
{
    private IShelfApiDbContext _dbContext = dbContext;

    public async Task<Result<List<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductDto> products = await _dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        return products;
    }
}