using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Models;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.Application.Common.Data;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<ApiError> ApiErrors { get; }

    DbSet<DomainEventOutboxMessage> DomainEventOutboxMessages { get; }
}