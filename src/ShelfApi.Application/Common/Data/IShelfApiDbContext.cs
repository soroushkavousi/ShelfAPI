using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Models;
using ShelfApi.Domain.CartDomain;
using ShelfApi.Domain.SettingDomain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Application.Common.Data;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<ProjectSetting> ProjectSettings { get; }

    DbSet<ApiError> ApiErrors { get; }

    DbSet<CartItem> CartItems { get; }

    DbSet<DomainEventOutboxMessage> DomainEventOutboxMessages { get; }
}