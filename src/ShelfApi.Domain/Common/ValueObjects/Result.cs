using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Domain.Common.ValueObjects;

public record Result
{
    protected Result() { }

    public Result(Error error)
    {
        Error = error;
    }

    public Result(ErrorCode errorCode) : this(new Error(errorCode)) { }

    public Error Error { get; set; }
    public bool HasError => Error is not null;
}

public record Result<TData> : Result
{
    public Result(Error error) : base(error) { }

    public Result(ErrorCode errorCode) : base(errorCode) { }

    public Result(TData data)
    {
        if (EqualityComparer<TData>.Default.Equals(data, default))
            throw new InvalidOperationException();

        Data = data;
    }

    public TData Data { get; init; }

    public static implicit operator Result<TData>(ErrorCode errorCode) => new(errorCode);
    public static implicit operator Result<TData>(Error error) => new(error);
    public static implicit operator Result<TData>(TData data) => new(data);

    public void Deconstruct(out Error error, out TData data)
        => (error, data) = (Error, Data);
}