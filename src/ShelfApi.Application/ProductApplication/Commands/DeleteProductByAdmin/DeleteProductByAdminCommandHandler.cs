using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.DeleteProductByAdmin;

public class DeleteProductByAdminCommandHandler(IShelfApiDbContext dbContext, IScopeService scopeService)
    : IRequestHandler<DeleteProductByAdminCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteProductByAdminCommand request, CancellationToken cancellationToken)
    {
        Product product = await dbContext.Products
            .Where(p => p.Id == request.Id && !p.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return ErrorCode.ItemNotFound;

        product.Delete();

        await dbContext.SaveChangesAsync(cancellationToken);

        scopeService.FireAndForget(new ProductDeletedEvent { Product = product.ToEventDto() });

        return true;
    }
}