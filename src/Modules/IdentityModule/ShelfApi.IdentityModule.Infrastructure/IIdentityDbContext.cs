using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.IdentityModule.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.IdentityModule.Infrastructure;

public interface IIdentityDbContext : IDbContext
{
    public DbSet<IdentityUserRole<long>> UserRoles { get; }
    public DbSet<Role> Roles { get; }
    public DbSet<User> Users { get; }
}