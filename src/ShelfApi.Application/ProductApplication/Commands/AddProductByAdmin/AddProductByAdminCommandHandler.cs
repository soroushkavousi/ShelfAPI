using Mapster;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication;

public class AddProductByAdminCommandHandler : ApiRequestHandler<AddProductByAdminCommand, ProductDto>
{
    private IShelfApiDbContext _dbContext;
    private IIdManager _idManager;

    public AddProductByAdminCommandHandler(IIdManager idManager, IShelfApiDbContext dbContext)
    {
        _dbContext = dbContext;
        _idManager = idManager;
    }

    protected override async Task<ProductDto> OperateAsync(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        ulong id = _idManager.GenerateNextUlong();
        Product product = new(id, request.Name, request.Price, request.Quantity);

        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }
}

