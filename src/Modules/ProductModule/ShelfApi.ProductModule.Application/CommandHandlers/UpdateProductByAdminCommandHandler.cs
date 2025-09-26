using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Mappers;
using ShelfApi.ProductModule.Contracts.Commands;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Application.CommandHandlers;

public class UpdateProductByAdminCommandHandler(IProductDbContext dbContext)
    : IRequestHandler<UpdateProductByAdminCommand, Result<ProductUserView>>
{
    public async Task<Result<ProductUserView>> Handle(UpdateProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = await dbContext.Products
            .Where(p => p.Id == request.Id && !p.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return ErrorCode.ItemNotFound;

        product.Update(request.Name, price, request.Quantity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return product.ToUserView();
    }
}