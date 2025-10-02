using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.CartModule.Application.Interfaces;
using ShelfApi.CartModule.Contracts.Queries;
using ShelfApi.CartModule.Contracts.Views;
using ShelfApi.CartModule.Domain;
using ShelfApi.FinancialModule.Contracts.Commands;
using ShelfApi.FinancialModule.Contracts.Views;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Application.QueryHandlers;

public class GetUserCartByUserQueryHandler(ICartDbContext dbContext, IMediator mediator)
    : IRequestHandler<GetUserCartByUserQuery, Result<CartUserView>>
{
    public async Task<Result<CartUserView>> Handle(GetUserCartByUserQuery request, CancellationToken cancellationToken)
    {
        CartItem[] cartItems = await dbContext.Carts
            .Include(x => x.Items)
            .Where(x => x.UserId == request.UserId)
            .Select(x => new CartItemUserView
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        CreatedAt = x.CreatedAt,
                        ModifiedAt = x.ModifiedAt
                    })
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        Result<PaymentPreviewView> paymentPreview = await mediator.Send(new CreatePaymentPreviewCommand
        {
            PaymentLines = cartItems.Select(x => new PaymentLine
            {
                Name = x.Product.Name,
                UnitPrice = x.Product.Price,
                Quantity = x.Quantity
            }).ToArray()
        });

        return paymentPreview;
    }
}