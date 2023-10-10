namespace ShelfApi.Application.ErrorApplication;

public abstract class ApiException : Exception
{
    protected ApiException(ErrorType type, ErrorField field, string message = null, Exception innerException = null) : base(message, innerException)
    {
        Type = type;
        Field = field;
    }

    protected ApiException(ErrorType type, ErrorField field, string message = null) : this(type, field, message, null)
    {
        Type = type;
        Field = field;
    }

    public ErrorType Type { get; set; }
    public ErrorField Field { get; set; }
}

public class InternalServerException : ApiException
{
    public InternalServerException(string message)
        : base(ErrorType.INTERNAL_SERVER, ErrorField.GENERAL, message) { }

    public InternalServerException(Exception innerException)
        : base(ErrorType.INTERNAL_SERVER, ErrorField.GENERAL, "something went wrong!", innerException) { }
}

public class NotExistException : ApiException
{
    public NotExistException(ErrorField field, object fieldValue)
        : base(ErrorType.NOT_EXIST, field, $"input {field} with value ({fieldValue}) does not exist.") { }
}

public class ALreadyExistsException : ApiException
{
    public ALreadyExistsException(ErrorField field, object fieldValue)
        : base(ErrorType.ALREADY_EXISTS, field, $"input {field} with value ({fieldValue}) already exists.") { }
}

public class InvalidFormatException : ApiException
{
    public InvalidFormatException(ErrorField field)
        : base(ErrorType.INVALID_FORMAT, field, $"input {field} format is not valid.") { }

    public InvalidFormatException(ErrorField field, object fieldValue)
        : base(ErrorType.INVALID_FORMAT, field, $"input {field} format with value ({fieldValue}) is not valid.") { }
}

public class InvalidValueException : ApiException
{
    public InvalidValueException(ErrorField field)
        : base(ErrorType.INVALID_VALUE, field, $"input {field} value is not valid.") { }

    public InvalidValueException(ErrorField field, object fieldValue)
        : base(ErrorType.INVALID_VALUE, field, $"input {field} value ({fieldValue}) is not valid.") { }
}
