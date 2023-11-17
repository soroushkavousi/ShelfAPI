using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.ConfigurationAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.Common;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<IdentityUserRole<ulong>> UserRoles { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Configs> Configs { get; set; }
}