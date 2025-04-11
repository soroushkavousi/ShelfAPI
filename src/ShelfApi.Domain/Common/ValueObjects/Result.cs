using Bitiano.Shared.ValueObjects;
using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Domain.Common.ValueObjects;

public record Result : BaseResult<Error>
{
    protected Result() { }

    public Result(Error error) : base(error) { }

    public Result(ErrorCode errorCode) : this(new Error(errorCode)) { }
}

public record Result<TData> : BaseResult<TData, Error>
{
    public Result(TData data) : base(data) { }
    public Result(TData data, Pagination pagination) : base(data, pagination) { }
    public Result(Error error) : base(error) { }
    public Result(ErrorCode errorCode) : base(new Error(errorCode)) { }

    public static implicit operator Result<TData>(TData data) => new(data);
    public static implicit operator Result<TData>(ErrorCode errorCode) => new(errorCode);
    public static implicit operator Result<TData>(Error error) => new(error);

    public static implicit operator Result<TData>((TData data, Pagination pagination) page) =>
        new(page.data, page.pagination);
}