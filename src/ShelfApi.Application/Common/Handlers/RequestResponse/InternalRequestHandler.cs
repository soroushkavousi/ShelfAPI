using MediatR;

namespace ShelfApi.Application.Common;

public abstract class InternalRequestHandler<TRequest, TResultData> : IRequestHandler<TRequest, TResultData>
    where TRequest : InternalRequest<TResultData>
{
    public async Task<TResultData> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var resultData = await OperateAsync(request, cancellationToken);
        return resultData;
    }

    protected abstract Task<TResultData> OperateAsync(TRequest request, CancellationToken cancellationToken);
}
