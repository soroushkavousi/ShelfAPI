using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.AuthApplication;

namespace ShelfApi.Application.ErrorApplication;

public class ExceptionHandler<TRequest, TResponse, TException>
    (ILogger<ExceptionHandler<TRequest, TResponse, TException>> logger, ISender sender)
    : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
    where TResponse : new()
{
    private readonly ILogger<ExceptionHandler<TRequest, TResponse, TException>> _logger = logger;
    private readonly ISender _sender = sender;

    public async Task Handle(TRequest request,
        TException exception,
        RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Command failed! request type: {RequestType}", request.GetType().FullName);

        // Handle the exceptions of internal requests
        if (state.GetType().GenericTypeArguments[0].IsAssignableFrom(typeof(ResultDto)))
            throw exception;

        ApiException apiException = exception is ApiException ? exception as ApiException : new InternalServerException(exception);
        ErrorDto error = await _sender.Send(new GetErrorQuery
        {
            ErrorType = apiException.Type,
            ErrorField = apiException.Field
        });

        TResponse response = new();
        ResultDto result = response as ResultDto;
        result.SetErrors([error]);
        state.SetHandled(response);
    }
}