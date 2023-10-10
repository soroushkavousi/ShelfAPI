using MediatR;

namespace ShelfApi.Application.Common;

public abstract class ApiRequest<TResultData> : IRequest<ResultDto<TResultData>>
{

}
