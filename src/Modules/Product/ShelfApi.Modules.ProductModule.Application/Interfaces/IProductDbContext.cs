using Microsoft.EntityFrameworkCore;
using ShelfApi.Modules.ProductModule.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Modules.ProductModule.Application.Interfaces;

public interface IProductDbContext : IDbContext
{
    DbSet<Product> Products { get; }
}