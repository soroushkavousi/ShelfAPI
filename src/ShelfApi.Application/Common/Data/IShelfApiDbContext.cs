using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.Common;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<IdentityUserRole<ulong>> UserRoles { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<Order> Orders { get; set; }
    DbSet<OrderLine> OrderLines { get; set; }

    DbSet<Configs> Configs { get; set; }
}