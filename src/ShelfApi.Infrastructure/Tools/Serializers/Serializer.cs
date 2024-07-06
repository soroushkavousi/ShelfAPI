using ShelfApi.Application.Common;

namespace ShelfApi.Infrastructure.Tools;

public class Serializer : ISerializer
{
    public string Serialize<T>(T data, bool ignoreSensitiveLimit = false)
        => data.ToJson(ignoreSensitiveLimit);

    public T Deserialize<T>(string serializedData)
        => serializedData.FromJson<T>();
}
