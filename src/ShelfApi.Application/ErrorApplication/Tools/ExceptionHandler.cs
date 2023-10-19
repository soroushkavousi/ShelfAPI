using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ShelfApi.Application.AuthApplication;

namespace ShelfApi.Application.ErrorApplication;

public class ExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TException : Exception
    where TResponse : new()
{
    private readonly ILogger<ExceptionHandler<TRequest, TResponse, TException>> _logger;
    private readonly ISender _sender;

    public ExceptionHandler(ILogger<ExceptionHandler<TRequest, TResponse, TException>> logger,
        ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public async Task Handle(TRequest request,
        TException exception,
        RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
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
        result.SetErrors(new List<ErrorDto> { error });
        state.SetHandled(response);
    }
}
