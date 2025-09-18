using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Modules.Identity.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Modules.Identity.Infrastructure;

public interface IIdentityDbContext : IDbContext
{
    public DbSet<IdentityUserRole<long>> UserRoles { get; }
    public DbSet<Role> Roles { get; }
    public DbSet<User> Users { get; }
}