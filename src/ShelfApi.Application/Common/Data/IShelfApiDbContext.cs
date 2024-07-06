using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.OrderAggregate;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.Common;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<ProjectSetting> ProjectSettings { get; set; }

    DbSet<IdentityUserRole<ulong>> UserRoles { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }

    DbSet<ApiError> ApiErrors { get; set; }

    DbSet<Product> Products { get; set; }

    DbSet<Order> Orders { get; set; }
    DbSet<OrderLine> OrderLines { get; set; }
}