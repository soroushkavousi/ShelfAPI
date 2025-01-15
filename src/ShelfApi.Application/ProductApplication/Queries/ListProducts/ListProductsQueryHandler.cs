using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public class ListProductsQueryHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<ListProductsQuery, Result<List<ProductDto>>>
{
    private readonly IShelfApiDbContext _dbContext = dbContext;

    public async Task<Result<List<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductDto> products = await _dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        return products;
    }
}