using Bitiano.Shared.ValueObjects;

namespace ShelfApi.Domain.ErrorAggregate;

public record Error : BaseError<ErrorCode, ErrorField>
{
    public Error(ErrorCode code, ErrorField? field = null) : base(code, field)
    {
    }

    public Error(ErrorCode code, string title, string message, ErrorField? field = null) : base(code, title, message, field)
    {
    }
}