using Microsoft.EntityFrameworkCore;
using ShelfApi.ProductModule.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.ProductModule.Application.Interfaces;

public interface IProductDbContext : IDbContext
{
    DbSet<Product> Products { get; }
}