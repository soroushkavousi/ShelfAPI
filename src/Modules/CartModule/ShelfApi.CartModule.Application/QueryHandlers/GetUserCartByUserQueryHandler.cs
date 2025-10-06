using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.CartModule.Application.Interfaces;
using ShelfApi.CartModule.Contracts.Queries;
using ShelfApi.CartModule.Contracts.Views;
using ShelfApi.FinancialModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Application.QueryHandlers;

public class GetUserCartByUserQueryHandler(ICartDbContext dbContext, IMediator mediator)
    : IRequestHandler<GetUserCartByUserQuery, Result<CartUserView>>
{
    public async Task<Result<CartUserView>> Handle(GetUserCartByUserQuery request,
        CancellationToken cancellationToken)
    {
        CartUserView cartUserView = await dbContext.Carts
            .Include(x => x.Items)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new CartUserView
            {
                UserId = x.UserId,
                Subtotal = x.Subtotal.Value,
                ModifiedAt = x.ModifiedAt,
                Items = x.Items.Select(y => new CartItemUserView
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    ProductId = y.ProductId,
                    UnitPrice = y.UnitPrice.Value,
                    Quantity = y.Quantity,
                    LinePrice = y.LinePrice.Value,
                    CreatedAt = y.CreatedAt,
                    ModifiedAt = y.ModifiedAt
                }).ToArray()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return cartUserView;
    }
}