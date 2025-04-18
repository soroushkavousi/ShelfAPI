using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.UpdateProductByAdmin;

public class UpdateProductByAdminCommandHandler(IShelfApiDbContext dbContext, IScopeService scopeService)
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

        scopeService.FireAndForget(new ProductUpdatedEvent { Product = product.ToEventDto() });

        return product.ToUserView();
    }
}