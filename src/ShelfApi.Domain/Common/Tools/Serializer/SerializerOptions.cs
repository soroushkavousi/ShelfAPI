using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShelfApi.Domain.Common.Tools.Serializer;

public static class SerializerOptions
{
    public static readonly JsonSerializerOptions Common = new();

    static SerializerOptions()
    {
        Common.AddCommonOptions();
    }

    public static void AddCommonOptions(this JsonSerializerOptions options)
    {
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
    }
}