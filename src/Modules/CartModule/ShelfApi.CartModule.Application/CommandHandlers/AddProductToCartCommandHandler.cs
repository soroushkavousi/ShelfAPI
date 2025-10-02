using DotNetPotion.SemaphorePoolPack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.CartModule.Application.Constants;
using ShelfApi.CartModule.Application.Interfaces;
using ShelfApi.CartModule.Contracts.Commands;
using ShelfApi.CartModule.Domain;
using ShelfApi.Shared.Common.Interfaces;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.CartModule.Application.CommandHandlers;

public class AddProductToCartCommandHandler(ICartDbContext dbContext,
    IIdGenerator idGenerator, ISemaphorePool semaphorePool)
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

            CartItem cartItem = new(idGenerator.GenerateId(), request.UserId, request.ProductId);
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