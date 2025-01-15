using ShelfApi.Application.BaseDataApplication.Interfaces;

namespace ShelfApi.Application.Common.Tools.MediatR;

public class ErrorResultPipelineBehavior<TRequest, TResponse>(IBaseDataService baseDataService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        if (response is not Result { Error: not null } result)
            return response;

        ApiError apiError = baseDataService.ApiErrors[result.Error.Code];
        result.Error.SetDetails(apiError.Title, apiError.Message);

        return response;
    }
}