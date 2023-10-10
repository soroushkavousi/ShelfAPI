using MediatR;

namespace ShelfApi.Application.Common;

public abstract class InternalRequest<TResultData> : IRequest<TResultData>
{

}
