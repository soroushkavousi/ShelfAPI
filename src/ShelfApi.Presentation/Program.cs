using ShelfApi.Application;
using ShelfApi.Infrastructure;
using ShelfApi.Presentation;
using ShelfApi.Presentation.Middlewares;
using ShelfApi.Presentation.Tools;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(o =>
{
    o.DocumentFilter<SwaggerDocumentFilter>();
});
services.AddPresentation();
services.AddInfrastructure(EnvironmentVariable.ConnectionString.Value);
services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiExceptionHandler();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();