using Ardalis.GuardClauses;
using ShelfApi.Domain.Common.Model;

namespace ShelfApi.Domain.ErrorAggregate;

public class ApiError : DomainModel
{
    public ApiError(ErrorCode code, string title, string message)
    {
        Code = Guard.Against.EnumOutOfRange(code);
        Title = Guard.Against.NullOrWhiteSpace(title);
        Message = Guard.Against.NullOrWhiteSpace(message);
    }

    public ErrorCode Code { get; }
    public string Title { get; }
    public string Message { get; }
}