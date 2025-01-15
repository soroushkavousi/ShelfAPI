using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShelfApi.Domain.ErrorAggregate;

namespace ShelfApi.Presentation.ActionFilters;

public class StatusCodeActionFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is Result result)
        {
            if (result.HasError)
            {
                if (result.Error.Code == ErrorCode.InternalServerError)
                {
                    objectResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.HttpContext.Response.StatusCode = 500;
                }
                else
                {
                    objectResult.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.HttpContext.Response.StatusCode = 400;
                }
            }
            else
            {
                objectResult.StatusCode = (int)HttpStatusCode.OK;
                context.HttpContext.Response.StatusCode = 200;
            }
        }

        await next();
    }
}