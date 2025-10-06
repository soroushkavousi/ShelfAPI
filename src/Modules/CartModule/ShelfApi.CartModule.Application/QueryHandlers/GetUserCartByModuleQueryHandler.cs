using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.CartModule.Application.Interfaces;
using ShelfApi.CartModule.Contracts.Queries;
using ShelfApi.CartModule.Contracts.Views;
using ShelfApi.FinancialModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Application.QueryHandlers;

public class GetUserCartByModuleQueryHandler(ICartDbContext dbContext, IMediator mediator)
    : IRequestHandler<GetUserCartByModuleQuery, CartModuleView>
{
    public async Task<CartModuleView> Handle(GetUserCartByModuleQuery request,
        CancellationToken cancellationToken)
    {
        CartModuleView cartModuleView = await dbContext.Carts
            .Include(x => x.Items)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new CartModuleView
            {
                UserId = x.UserId,
                Subtotal = x.Subtotal.Value,
                ModifiedAt = x.ModifiedAt,
                Items = x.Items.Select(y => new CartItemModuleView
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

        return cartModuleView;
    }
}