using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Domain.Common.Exceptions;

public class ServerException : Exception
{
    public ServerException(Error error) : base($"Faced error {error}")
    {
        Error = error;
    }

    public ServerException(ErrorCode code, ErrorField? field = null) : this(new Error(code, field))
    {
    }

    public ServerException(Error error, object value) : base($"Faced error {error} with value {value}")
    {
        Error = error;
    }

    public ServerException(ErrorCode code, object value, ErrorField? field = null) : this(new Error(code, field), value)
    {
    }

    public ServerException(string message) : base(message)
    {
    }

    public Error Error { get; set; }
}