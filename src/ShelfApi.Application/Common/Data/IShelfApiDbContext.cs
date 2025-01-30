﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.CartDomain;
using ShelfApi.Domain.ProductAggregate;
using ShelfApi.Domain.UserAggregate;

namespace ShelfApi.Application.Common.Data;

public interface IShelfApiDbContext : IDbContext
{
    DbSet<ProjectSetting> ProjectSettings { get; }

    DbSet<IdentityUserRole<long>> UserRoles { get; }
    DbSet<Role> Roles { get; }
    DbSet<User> Users { get; }

    DbSet<ApiError> ApiErrors { get; }

    DbSet<Product> Products { get; }

    DbSet<CartItem> CartItems { get; }
}