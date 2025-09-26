using MediatR;
using ShelfApi.ProductModule.Application.Interfaces;
using ShelfApi.ProductModule.Application.Mappers;
using ShelfApi.ProductModule.Contracts.Commands;
using ShelfApi.ProductModule.Contracts.Views;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.Interfaces;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.ProductModule.Application.CommandHandlers;

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