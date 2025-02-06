namespace ShelfApi.Application.ErrorApplication.Queries.GetApiError;

public class GetApiErrorQuery : IRequest<ApiError>
{
    public required ErrorCode ErrorCode { get; init; }
}