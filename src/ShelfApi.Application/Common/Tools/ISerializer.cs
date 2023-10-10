namespace ShelfApi.Application.Common;

public interface ISerializer
{
    public string Serialize<T>(T data, bool ignoreSensitiveLimit = false);
    public T Deserialize<T>(string serializedData);
}
