using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ShelfApi.Domain.Common.ValueObjects;

namespace ShelfApi.Application.Common.Tools.MediatR;

public class ExceptionHandler<TRequest, TResponse, TException>
    (ILogger<ExceptionHandler<TRequest, TResponse, TException>> logger)
    : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
    where TResponse : Result, new()
{
    private readonly ILogger<ExceptionHandler<TRequest, TResponse, TException>> _logger = logger;

    public Task Handle(TRequest request,
        TException exception,
        RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Command failed! request type: {RequestType}", request.GetType().FullName);

        Result result = new Result(ErrorCode.InternalServerError);
        TResponse response = (TResponse)result;
        state.SetHandled(response);

        return Task.CompletedTask;
    }
}