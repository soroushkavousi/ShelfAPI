using MediatR;
using ShelfApi.Application.AuthApplication;
using ShelfApi.Application.Common;
using ShelfApi.Application.ErrorApplication;
using System.Diagnostics;
using System.Net;

namespace ShelfApi.Presentation.Middlewares;

public class ApiExceptionHandlerMiddleware
{
    private readonly ILogger<ApiExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly ISerializer _serializer;

    public ApiExceptionHandlerMiddleware(ILogger<ApiExceptionHandlerMiddleware> logger,
        RequestDelegate next, ISerializer serializer)
    {
        _logger = logger;
        _next = next;
        _serializer = serializer;
    }

    public async Task InvokeAsync(HttpContext httpContext, ISender sender)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            ApiException apiException = ex is ApiException ? ex as ApiException : new InternalServerException(ex);
            ErrorDto error = await sender.Send(new GetErrorQuery
            {
                ErrorType = apiException.Type,
                ErrorField = apiException.Field
            });
            await ReportError(error, httpContext);
        }
    }

    private async Task ReportError(ErrorDto error, HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = error.Type == ErrorType.INTERNAL_SERVER ? (int)HttpStatusCode.InternalServerError : (int)HttpStatusCode.BadRequest;
        string responseBody = _serializer.Serialize(error, true);
        await httpContext.Response.WriteAsync(responseBody);
    }
}

public static class IApplicationBuilderApiExceptionHandlerMiddleware
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiExceptionHandlerMiddleware>();
    }
}
