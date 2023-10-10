using ShelfApi.Application.ErrorApplication;

namespace ShelfApi.Application.Common;

public abstract record ResultDto
{
    public List<ErrorDto> Errors { get; protected set; }

    public abstract void SetErrors(List<ErrorDto> errors);
}

public record ResultDto<TData> : ResultDto
{
    public TData Data { get; init; }

    public ResultDto() { }

    private ResultDto(TData data)
    {
        Data = data;
    }

    public static ResultDto<TData> Success(TData data)
        => new(data);

    public override void SetErrors(List<ErrorDto> errors)
    {
        if (Data != null)
            throw new InvalidOperationException();

        Errors = errors;
    }
}