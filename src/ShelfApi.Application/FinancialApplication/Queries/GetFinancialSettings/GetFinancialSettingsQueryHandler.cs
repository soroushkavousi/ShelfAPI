using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Application.SettingApplication;
using ShelfApi.Domain.FinancialAggregate;
using ShelfApi.Shared.Common.Exceptions;
using ShelfApi.Shared.Common.Tools.Serializer;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.FinancialApplication.Queries.GetFinancialSettings;

public class GetFinancialSettingsQueryHandler(IShelfApiDbContext shelfApiDbContext, IFusionCache cache)
    : IRequestHandler<GetFinancialSettingsQuery, FinancialSettings>
{
    public async Task<FinancialSettings> Handle(GetFinancialSettingsQuery request, CancellationToken cancellationToken)
    {
        FinancialSettings financialSettings = await cache.GetOrSetAsync(
            "financialSettings",
            _ => GetFinancialSettingsFromDatabaseAsync(cancellationToken),
            options => options
                .SetDuration(TimeSpan.FromMinutes(2))
                .SetDistributedCacheDuration(TimeSpan.FromHours(12))
        );

        if (financialSettings is null)
            throw new ServerException("Could not fetch financial settings");

        return financialSettings;
    }

    public async Task<FinancialSettings> GetFinancialSettingsFromDatabaseAsync(CancellationToken cancellationToken)
    {
        string financialSettingsJson = await shelfApiDbContext.ProjectSettings
            .Where(x => x.Key == ProjectSettingKeys.FinancialSettings)
            .Select(x => x.Value)
            .FirstOrDefaultAsync(cancellationToken);

        return string.IsNullOrWhiteSpace(financialSettingsJson)
            ? null
            : financialSettingsJson.FromJson<FinancialSettings>();
    }
}