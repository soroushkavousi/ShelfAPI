using System.Net;
using MediatR;
using ShelfApi.Application.ErrorApplication.Queries.GetApiError;
using ShelfApi.Domain.ErrorAggregate;
using ShelfApi.Shared.Common.Tools.Serializer;

namespace ShelfApi.Presentation.Middlewares;

public class ApiExceptionHandlerMiddleware(ILogger<ApiExceptionHandlerMiddleware> logger,
    RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext, ISender sender)
    {
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

        IMediator mediator = httpContext.RequestServices.GetRequiredService<IMediator>();
        ApiError apiError = await mediator.Send(new GetApiErrorQuery { ErrorCode = ErrorCode.InternalServerError });
        Result<object> result = new Error(apiError.Code, apiError.Title, apiError.Message);
        string responseBody = result.ToJson(true);
        await httpContext.Response.WriteAsync(responseBody);
    }
}

public static class ApplicationBuilderApiExceptionHandlerMiddleware
{
    public static void UseApiExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ApiExceptionHandlerMiddleware>();
    }
}