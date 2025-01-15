using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Dtos;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommandHandler(IShelfApiDbContext dbContext)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductDto>>
{
    private readonly IShelfApiDbContext _dbContext = dbContext;

    public async Task<Result<ProductDto>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = new(request.Name, price, request.Quantity);

        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }
}