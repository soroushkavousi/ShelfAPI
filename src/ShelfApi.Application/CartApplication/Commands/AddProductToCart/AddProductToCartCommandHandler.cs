using DotNetPotion.SemaphorePoolPack;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Constants;
using ShelfApi.Application.Common.Data;
using ShelfApi.Domain.CartDomain;

namespace ShelfApi.Application.CartApplication.Commands.AddProductToCart;

public class AddProductToCartCommandHandler(IShelfApiDbContext dbContext, ISemaphorePool semaphorePool)
    : IRequestHandler<AddProductToCartCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        SemaphoreSlim semaphore = await semaphorePool.WaitAsync(LockKeys.GetAddProductToCartLockKey(request.UserId));
        try
        {
            CartItem existingCartItem = await dbContext.CartItems
                .Where(x => x.UserId == request.UserId && x.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingCartItem is not null)
            {
                existingCartItem.IncreaseQuantity();
                await dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }

            CartItem cartItem = new(request.UserId, request.ProductId);
            dbContext.CartItems.Add(cartItem);
            await dbContext.SaveChangesAsync();

            return true;
        }
        finally
        {
            semaphore.Release();
        }
    }
}