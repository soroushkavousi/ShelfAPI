using DotNetPotion.ScopeServicePack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.Common.Data;

namespace ShelfApi.Application.BaseDataApplication.Services;

public class BaseDataService(IScopeService scopeService) : IBaseDataService
{
    public Dictionary<ErrorCode, ApiError> ApiErrors { get; private set; }

    public async Task InitializeAsync()
    {
        List<Task> tasks =
        [
            LoadApiErrorsAsync()
        ];

        await Task.WhenAll(tasks);
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