using Bitiano.Shared.ValueObjects;
using ShelfApi.Application.ErrorApplication.Queries.GetApiError;

namespace ShelfApi.Application.Common.Tools.MediatR;

public class ErrorResultPipelineBehavior<TRequest, TResponse>(IMediator mediator)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        if (response is not BaseResult<Error> { Error: not null } result)
            return response;

        ApiError apiError = await mediator.Send(new GetApiErrorQuery { ErrorCode = result.Error.Code });
        result.Error.SetDetails(apiError.Title, apiError.Message);

        return response;
    }
}