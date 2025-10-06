using Microsoft.EntityFrameworkCore;
using ShelfApi.CartModule.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.CartModule.Application.Interfaces;

public interface ICartDbContext : IDbContext
{
    DbSet<Cart> Carts { get; }
    DbSet<CartItem> CartItems { get; }
}