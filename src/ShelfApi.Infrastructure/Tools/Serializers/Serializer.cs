using ShelfApi.Application.Common.Tools;

namespace ShelfApi.Infrastructure.Tools.Serializers;

public class Serializer : ISerializer
{
    public string Serialize<T>(T data, bool ignoreSensitiveLimit = false)
        => data.ToJson(ignoreSensitiveLimit);

    public T Deserialize<T>(string serializedData)
        => serializedData.FromJson<T>();
}