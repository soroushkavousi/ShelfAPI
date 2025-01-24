using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.Common.Data;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common.Tools.Serializer;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataService(IScopeService scopeService) : IBaseDataService
{
    public FinancialSettings FinancialSettings { get; private set; }
    public Dictionary<ErrorCode, ApiError> ApiErrors { get; private set; }

    public async Task InitializeAsync()
    {
        List<Task> tasks =
        [
            LoadProjectSettingsAsync(),
            LoadApiErrorsAsync()
        ];

        await Task.WhenAll(tasks);
    }

    public async Task LoadProjectSettingsAsync()
    {
        await scopeService.Run(async scope =>
        {
            IShelfApiDbContext shelfApiDbContext = scope.ServiceProvider.GetRequiredService<IShelfApiDbContext>();
            Dictionary<ProjectSettingId, string> projectSettings = await shelfApiDbContext.ProjectSettings
                .ToDictionaryAsync(x => x.Id, x => x.Data);

            FinancialSettings = projectSettings[ProjectSettingId.FinancialSettings].FromJson<FinancialSettings>();
        });
    }

    public async Task LoadApiErrorsAsync()
    {
        await scopeService.Run(async scope =>
        {
            IShelfApiDbContext shelfApiDbContext = scope.ServiceProvider.GetRequiredService<IShelfApiDbContext>();
            ApiErrors = await shelfApiDbContext.ApiErrors
                .AsNoTracking()
                .ToDictionaryAsync(x => x.Code, x => x);

            ErrorCode[] storedErrorCodes = ApiErrors.Keys.ToArray();
            ErrorCode[] allErrorCodes = Enum.GetValues<ErrorCode>();
            IEnumerable<ErrorCode> missingErrorCodes = allErrorCodes.Except(storedErrorCodes);

            if (missingErrorCodes.Any())
                throw new InvalidDataException($"Missing error codes: [{string.Join(", ", missingErrorCodes)}]");
        });
    }
}