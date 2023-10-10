namespace ShelfApi.Application.ErrorApplication;

public record ErrorDto(int Code, ErrorType Type, ErrorField Field, string Message);
