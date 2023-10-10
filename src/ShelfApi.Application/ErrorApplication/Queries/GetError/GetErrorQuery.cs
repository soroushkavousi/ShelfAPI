using ShelfApi.Application.ErrorApplication;

namespace ShelfApi.Application.AuthApplication;

public class GetErrorQuery : InternalRequest<ErrorDto>
{
    public ErrorType ErrorType { get; init; }
    public ErrorField ErrorField { get; init; }
}
