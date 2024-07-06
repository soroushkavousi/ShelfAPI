using ShelfApi.Application.BaseDataApplication.Interfaces;

namespace ShelfApi.Application.Common.Tools.MediatR;

public class ErrorResultPipelineBehavior<TRequest, TResponse>(IBaseDataService baseDataService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IBaseDataService _baseDataService = baseDataService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        if (response is Result result && result.Error is not null)
        {
            ApiError apiError = _baseDataService.ApiErrors[result.Error.Code];
            result.Error.SetDetails(apiError.Title, apiError.Message);
        }

        return response;
    }
}