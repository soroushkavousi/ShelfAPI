using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataService : IBaseDataService
{
    public JwtSettings JwtSettings { get; private set; }
    public FinancialSettings FinancialSettings { get; private set; }

    public async Task InitializeAsync(IShelfApiDbContext shelfApiDbContext)
    {
        List<Task> tasks = [];

        tasks.Add(LoadProjectSettingsAsync(shelfApiDbContext));

        await Task.WhenAll(tasks);
    }

    private async Task LoadProjectSettingsAsync(IShelfApiDbContext shelfApiDbContext)
    {
        Dictionary<ProjectSettingId, string> projectSettings = await shelfApiDbContext.ProjectSettings
            .ToDictionaryAsync(x => x.Id, x => x.Data);

        JwtSettings = projectSettings[ProjectSettingId.JWT].FromJson<JwtSettings>();
        FinancialSettings = projectSettings[ProjectSettingId.FINANCIAL].FromJson<FinancialSettings>();
    }
}