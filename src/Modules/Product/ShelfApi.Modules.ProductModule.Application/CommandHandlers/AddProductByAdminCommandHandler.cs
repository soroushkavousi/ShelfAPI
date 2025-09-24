using MediatR;
using ShelfApi.Modules.ProductModule.Application.Interfaces;
using ShelfApi.Modules.ProductModule.Application.Mappers;
using ShelfApi.Modules.ProductModule.Contracts.Commands;
using ShelfApi.Modules.ProductModule.Contracts.Views;
using ShelfApi.Modules.ProductModule.Domain;
using ShelfApi.Shared.Common.Interfaces;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Modules.ProductModule.Application.CommandHandlers;

public class AddProductByAdminCommandHandler(IProductDbContext dbContext, IIdGenerator idGenerator)
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