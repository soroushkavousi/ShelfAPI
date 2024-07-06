using Bitiano.DevKit.Services.TaskServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Domain.BaseDataAggregate;
using ShelfApi.Domain.Common;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataService(ITaskService taskService) : IBaseDataService
{
    private readonly ITaskService _taskService = taskService;

    public JwtSettings JwtSettings { get; private set; }
    public FinancialSettings FinancialSettings { get; private set; }
    public Dictionary<ErrorCode, ApiError> ApiErrors { get; private set; }

    public async Task InitializeAsync()
    {
        List<Task> tasks = [];

        tasks.Add(LoadProjectSettingsAsync());
        tasks.Add(LoadApiErrorsAsync());

        await Task.WhenAll(tasks);
    }

    public async Task LoadProjectSettingsAsync()
    {
        await _taskService.RunInANewThread(async (scope) =>
        {
            IShelfApiDbContext shelfApiDbContext = scope.ServiceProvider.GetRequiredService<IShelfApiDbContext>();
            Dictionary<ProjectSettingId, string> projectSettings = await shelfApiDbContext.ProjectSettings
                .ToDictionaryAsync(x => x.Id, x => x.Data);

            JwtSettings = projectSettings[ProjectSettingId.JWT].FromJson<JwtSettings>();
            FinancialSettings = projectSettings[ProjectSettingId.FINANCIAL].FromJson<FinancialSettings>();
        });
    }

    public async Task LoadApiErrorsAsync()
    {
        await _taskService.RunInANewThread(async (scope) =>
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