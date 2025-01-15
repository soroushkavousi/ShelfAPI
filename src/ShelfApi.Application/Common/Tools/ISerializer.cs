namespace ShelfApi.Application.Common.Tools;

public interface ISerializer
{
    public string Serialize<T>(T data, bool ignoreSensitiveLimit = false);
    public T Deserialize<T>(string serializedData);
}