using Microsoft.EntityFrameworkCore;
using ShelfApi.Application.Common.Data;
using ShelfApi.Shared.Common.Exceptions;
using ZiggyCreatures.Caching.Fusion;

namespace ShelfApi.Application.ErrorApplication.Queries.GetApiError;

public class GetApiErrorQueryHandler(IShelfApiDbContext dbContext, IFusionCache cache)
    : IRequestHandler<GetApiErrorQuery, ApiError>
{
    public async Task<ApiError> Handle(GetApiErrorQuery request, CancellationToken cancellationToken)
    {
        Dictionary<ErrorCode, ApiError> apiErrors = await cache.GetOrSetAsync(
            "apiErrors",
            _ => GetApiErrorsFromDatabaseAsync(cancellationToken),
            options => options
                .SetDuration(TimeSpan.FromMinutes(2))
                .SetDistributedCacheDuration(TimeSpan.FromHours(12))
        );

        if (!apiErrors.TryGetValue(request.ErrorCode, out ApiError apiError))
            throw new ServerException($"Could not fetch api error {request.ErrorCode}");

        return apiError;
    }

    public async Task<Dictionary<ErrorCode, ApiError>> GetApiErrorsFromDatabaseAsync(CancellationToken cancellationToken)
    {
        Dictionary<ErrorCode, ApiError> apiErrors = await dbContext.ApiErrors
            .AsNoTracking()
            .ToDictionaryAsync(x => x.Code, x => x, cancellationToken);

        return apiErrors;
    }
}