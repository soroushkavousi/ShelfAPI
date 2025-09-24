using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Modules.ProductModule.Application.Interfaces;
using ShelfApi.Modules.ProductModule.Contracts.Commands;
using ShelfApi.Modules.ProductModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.ProductModule.Application.CommandHandlers;

public class DeleteProductByAdminCommandHandler(IProductDbContext dbContext)
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

        return true;
    }
}