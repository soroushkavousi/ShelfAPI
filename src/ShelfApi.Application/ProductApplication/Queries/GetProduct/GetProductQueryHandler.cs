using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;

namespace ShelfApi.Application.ProductApplication.Queries.GetProduct;

public class GetProductQueryHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<GetProductQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ProductDto product = await dbContext.Products
            .Where(x => x.Id == request.Id)
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return ErrorCode.ItemNotFound;

        return product;
    }
}