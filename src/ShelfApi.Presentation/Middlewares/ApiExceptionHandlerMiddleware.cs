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
    public async Task InvokeAsync(HttpContext httpContext, ISender sender)
    {
        Stopwatch sw = Stopwatch.StartNew();
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await MakeResponseInternalServerErrorAsync(ex, httpContext);
        }
    }

    private async Task MakeResponseInternalServerErrorAsync(Exception ex, HttpContext httpContext)
    {
        logger.LogError(ex, "Could not process the request!");

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        ApiError apiError = baseDataService.ApiErrors[ErrorCode.InternalServerError];
        Result<object> result = new Error(apiError.Code, apiError.Title, apiError.Message);
        string responseBody = serializer.Serialize(result, true);
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