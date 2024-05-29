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

        tasks.Add(LoadMainSettingsAsync(shelfApiDbContext));

        await Task.WhenAll(tasks);
    }

    private async Task LoadMainSettingsAsync(IShelfApiDbContext shelfApiDbContext)
    {
        Dictionary<MainSettingsCategory, string> mainSettings = await shelfApiDbContext.MainSettings
            .ToDictionaryAsync(x => x.Category, x => x.Data);

        JwtSettings = mainSettings[MainSettingsCategory.JWT].FromJson<JwtSettings>();
        FinancialSettings = mainSettings[MainSettingsCategory.FINANCIAL].FromJson<FinancialSettings>();
    }
}