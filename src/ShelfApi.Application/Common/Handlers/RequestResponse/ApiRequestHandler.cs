using MediatR;

namespace ShelfApi.Application.Common;

public abstract class ApiRequestHandler<TRequest, TResultData> : IRequestHandler<TRequest, ResultDto<TResultData>>
    where TRequest : ApiRequest<TResultData>
{
    public async Task<ResultDto<TResultData>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var resultData = await OperateAsync(request, cancellationToken);
        return ResultDto<TResultData>.Success(resultData);
    }

    protected abstract Task<TResultData> OperateAsync(TRequest request, CancellationToken cancellationToken);
}
