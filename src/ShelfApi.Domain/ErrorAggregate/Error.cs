using Ardalis.GuardClauses;

namespace ShelfApi.Domain.ErrorAggregate;

public record Error
{
    public Error(ErrorCode code, ErrorField? field = null)
    {
        Code = Guard.Against.EnumOutOfRange(code);
        if (field.HasValue)
            Field = Guard.Against.EnumOutOfRange(field.Value);
        CreatedAt = DateTime.UtcNow;
    }

    public Error(ErrorCode code, string title, string message, ErrorField? field = null)
        : this(code, field)
    {
        SetDetails(title, message);
    }

    public ErrorCode Code { get; }
    public string Title { get; private set; }
    public ErrorField? Field { get; }
    public string Message { get; private set; }
    public DateTime CreatedAt { get; }

    public void SetDetails(string title, string message)
    {
        Title = title;
        Message = message;
    }
}