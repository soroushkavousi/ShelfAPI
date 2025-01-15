using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Queries.ListProducts;

public class ListProductsQueryHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<ListProductsQuery, Result<List<ProductDto>>>
{
    public async Task<Result<List<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        List<ProductDto> products = await dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        return products;
    }
}