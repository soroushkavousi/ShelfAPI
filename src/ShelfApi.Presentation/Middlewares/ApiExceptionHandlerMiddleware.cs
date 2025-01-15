using System.Diagnostics;
using System.Net;
using MediatR;
using ShelfApi.Application.BaseDataApplication.Interfaces;
using ShelfApi.Application.Common.Tools;
using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Presentation.Middlewares;

public class ApiExceptionHandlerMiddleware(ILogger<ApiExceptionHandlerMiddleware> logger,
    RequestDelegate next, IBaseDataService baseDataService, ISerializer serializer)
{
    private readonly ILogger<ApiExceptionHandlerMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;
    private readonly IBaseDataService _baseDataService = baseDataService;
    private readonly ISerializer _serializer = serializer;

    public async Task InvokeAsync(HttpContext httpContext, ISender sender)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await MakeResponseInternalServerErrorAsync(ex, httpContext);
        }
    }

    private async Task MakeResponseInternalServerErrorAsync(Exception ex, HttpContext httpContext)
    {
        _logger.LogError(ex, "Could not process the request!");

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        ApiError apiError = _baseDataService.ApiErrors[ErrorCode.InternalServerError];
        Result<object> result = new Error(apiError.Code, apiError.Title, apiError.Message);
        string responseBody = _serializer.Serialize(result, true);
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