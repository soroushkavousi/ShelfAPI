using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication;

public class AddProductByAdminCommandHandler(IIdManager idManager, IShelfApiDbContext dbContext)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductDto>>
{
    private IIdManager _idManager = idManager;
    private IShelfApiDbContext _dbContext = dbContext;

    public async Task<Result<ProductDto>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        ulong id = _idManager.GenerateNextUlong();
        Product product = new(id, request.Name, price, request.Quantity);

        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }
}