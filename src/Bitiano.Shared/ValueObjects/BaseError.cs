using Ardalis.GuardClauses;

namespace Bitiano.Shared.ValueObjects;

public abstract record BaseError<TErrorCode>
    where TErrorCode : struct, Enum
{
    protected BaseError(TErrorCode code)
    {
        Code = Guard.Against.EnumOutOfRange(code);
        Timestamp = DateTime.UtcNow;
    }

    protected BaseError(TErrorCode code, string title, string message)
        : this(code)
    {
        SetDetails(title, message);
    }

    public TErrorCode Code { get; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public DateTime Timestamp { get; }

    public void SetDetails(string title, string message)
    {
        Title = title;
        Message = message;
    }
}

public abstract record BaseError<TErrorCode, TErrorField> : BaseError<TErrorCode>
    where TErrorCode : struct, Enum
    where TErrorField : struct, Enum
{
    protected BaseError(TErrorCode code, TErrorField? field = null) : base(code)
    {
        if (field.HasValue)
            Field = Guard.Against.EnumOutOfRange(field.Value);
    }

    protected BaseError(TErrorCode code, string title, string message, TErrorField? field = null)
        : base(code, title: title, message: message)
    {
        if (field.HasValue)
            Field = Guard.Against.EnumOutOfRange(field.Value);
    }

    public TErrorField? Field { get; }
}