using ShelfApi.Application.Common.Data;
using ShelfApi.Application.ProductApplication.Models.Views.UserViews;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Application.ProductApplication.Commands.AddProductByAdmin;

public class AddProductByAdminCommandHandler(IShelfApiDbContext dbContext, IIdGenerator idGenerator)
    : IRequestHandler<AddProductByAdminCommand, Result<ProductUserView>>
{
    public async Task<Result<ProductUserView>> Handle(AddProductByAdminCommand request, CancellationToken cancellationToken)
    {
        (Error error, Price price) = Price.TryCreate(request.Price);
        if (error is not null)
            return error;

        Product product = new(idGenerator.GenerateId(), request.Name, price, request.Quantity);

        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

        return product.ToUserView();
    }
}