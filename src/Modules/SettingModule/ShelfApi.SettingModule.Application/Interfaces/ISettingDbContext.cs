using Microsoft.EntityFrameworkCore;
using ShelfApi.SettingModule.Domain;
using ShelfApi.Shared.Common.Interfaces;

namespace ShelfApi.SettingModule.Application.Interfaces;

public interface ISettingDbContext : IDbContext
{
    DbSet<ProjectSetting> ProjectSettings { get; }
}