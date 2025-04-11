namespace Bitiano.Shared.ValueObjects;

public abstract record BaseResult<TError>
{
    protected BaseResult() { }

    protected BaseResult(TError error)
    {
        Error = error;
    }

    public TError Error { get; init; }
    public bool HasError => Error is not null;
}

public abstract record BaseResult<TData, TError> : BaseResult<TError>
{
    protected BaseResult(TData data)
    {
        if (EqualityComparer<TData>.Default.Equals(data, default))
            throw new InvalidOperationException();

        Data = data;
    }

    protected BaseResult(TData data, Pagination pagination)
    {
        if (EqualityComparer<TData>.Default.Equals(data, default))
            throw new InvalidOperationException();

        Data = data;
        Pagination = pagination;
    }

    protected BaseResult(TError error) : base(error) { }

    public TData Data { get; init; }
    public Pagination Pagination { get; init; }

    public void Deconstruct(out TError error, out TData data)
        => (error, data) = (Error, Data);
}