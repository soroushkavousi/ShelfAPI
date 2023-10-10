using ShelfApi.Application.ErrorApplication;

namespace ShelfApi.Application.AuthApplication;

public class GetErrorQueryHandler : InternalRequestHandler<GetErrorQuery, ErrorDto>
{
    private readonly IShelfApiDbContext _context;

    public GetErrorQueryHandler(IShelfApiDbContext context)
    {
        _context = context;
    }

    protected override async Task<ErrorDto> OperateAsync(GetErrorQuery request, CancellationToken cancellationToken)
    {
        var errorCode = 1;
        var errorMessage = "ERROR";
        var error = new ErrorDto(errorCode, request.ErrorType, request.ErrorField, errorMessage);
        return await Task.FromResult(error);
    }
}

