using MediatR;
using ShelfApi.FinancialModule.Application.ValueObjects;
using ShelfApi.SettingModule.Contracts.Queries;
using ShelfApi.Shared.Common.Exceptions;
using ShelfApi.Shared.Common.Tools.Serializer;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.FinancialModule.Application.QueryHandlers;

public class GetFinancialSettingsQuery : IRequest<FinancialSettings>;

public class GetFinancialSettingsQueryHandler(IMediator mediator, IFusionCache cache)
    : IRequestHandler<GetFinancialSettingsQuery, FinancialSettings>
{
    public async Task<FinancialSettings> Handle(GetFinancialSettingsQuery request, CancellationToken cancellationToken)
    {
        FinancialSettings financialSettings = await cache.GetOrSetAsync(
            "financialSettings",
            _ => GetFinancialSettingsFromSettingModuleAsync(cancellationToken),
            options => options
                .SetDuration(TimeSpan.FromMinutes(2))
                .SetDistributedCacheDuration(TimeSpan.FromHours(12))
        );

        if (financialSettings is null)
            throw new ServerException("Could not fetch financial settings");

        return financialSettings;
    }

    public async Task<FinancialSettings> GetFinancialSettingsFromSettingModuleAsync(CancellationToken cancellationToken)
    {
        string financialSettingsJson = await mediator.Send(new GetProjectSettingQuery
        {
            Key = "FinancialSettings"
        });

        return string.IsNullOrWhiteSpace(financialSettingsJson)
            ? null
            : financialSettingsJson.FromJson<FinancialSettings>();
    }
}