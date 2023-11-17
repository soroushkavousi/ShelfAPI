using System.Text.Json;

namespace ShelfApi.Domain.Common;

public static class SerializarExtensions
{
    public static string ToJson(this object obj)
    {
        if (obj == null)
            return null;

        return JsonSerializer.Serialize(obj, SerializerOptions.Common);
    }

    public static T FromJson<T>(this string json)
    {
        if (json == null)
            return default;

        return JsonSerializer.Deserialize<T>(json, SerializerOptions.Common);
    }
}
