using System.Text.Json;
using ShelfApi.Infrastructure.Tools.Serializers.NamingPolicies;

namespace ShelfApi.Infrastructure.Tools.Serializers;

public static class Extensions
{
    public static string ToJson(this object obj, bool ignoreSensitiveLimit = false)
    {
        if (obj == null)
            return null;

        JsonSerializerOptions serializerOptions = ignoreSensitiveLimit ? SerializerOptions.Common : SerializerOptions.Sensitive;
        return JsonSerializer.Serialize(obj, serializerOptions);
    }

    public static T FromJson<T>(this string json)
    {
        if (json == null)
            return default;
        return JsonSerializer.Deserialize<T>(json, SerializerOptions.Common);
    }

    public static string ToSnakeCase(this string str) =>
        SnakeCaseNamingPolicy.Instance.ConvertName(str);

    public static string ToKebabCase(this string str) =>
        KebabCaseNamingPolicy.Instance.ConvertName(str);
}