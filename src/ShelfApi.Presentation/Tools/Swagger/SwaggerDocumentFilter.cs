using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ShelfApi.Presentation.Tools.Swagger;

internal class SwaggerDocumentFilter : IDocumentFilter
{
    private readonly string _swaggerDocHost;

    public SwaggerDocumentFilter(IHttpContextAccessor httpContextAccessor)
    {
        var host = httpContextAccessor.HttpContext.Request.Host.Value;
        var scheme = httpContextAccessor.HttpContext.Request.Scheme;
        _swaggerDocHost = $"{scheme}://{host}";
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers.Add(new OpenApiServer { Url = _swaggerDocHost });
    }
}