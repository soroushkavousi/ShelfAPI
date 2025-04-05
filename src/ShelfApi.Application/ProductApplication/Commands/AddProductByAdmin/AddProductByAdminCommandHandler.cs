using DotNetPotion.ScopeServicePack;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Events;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommandHandler(IShelfApiDbContext dbContext, IScopeService scopeService)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductUserView>>
{
    public async Task<Result<ProductUserView>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = new(request.Name, price, request.Quantity);

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

        scopeService.FireAndForget(product.ToCreatedEvent());

        return product.Adapt<ProductUserView>();
    }
}