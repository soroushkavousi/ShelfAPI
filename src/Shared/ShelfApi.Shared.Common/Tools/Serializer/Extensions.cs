using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using ShelfApi.Shared.Common.Tools.Serializer.Modifiers;

namespace ShelfApi.Shared.Common.Tools.Serializer;

public static class Extensions
{
    private static readonly JsonSerializerOptions _defaultOptions;
    private static readonly JsonSerializerOptions _sensitiveOptions;

    static Extensions()
    {
        _defaultOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        _sensitiveOptions = new(_defaultOptions)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { SensitiveModifier.Modify }
            }
        };
    }

    public static string ToJson(this object obj, bool ignoreSensitiveLimit = false)
    {
        if (obj == null)
            return null;

        JsonSerializerOptions serializerOptions = ignoreSensitiveLimit ? _defaultOptions : _sensitiveOptions;
        return JsonSerializer.Serialize(obj, serializerOptions);
    }

    public static T FromJson<T>(this string json)
        => json == null ? default : JsonSerializer.Deserialize<T>(json, _defaultOptions);

    public static object FromJson(this string json, Type type)
        => json == null ? null : JsonSerializer.Deserialize(json, type, _defaultOptions);

    public static string ToCamelCase(this string name)
        => JsonNamingPolicy.CamelCase.ConvertName(name);
}