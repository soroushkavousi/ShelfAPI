namespace ShelfApi.Presentation.Tools;

public static class EnvironmentVariables
{
    public static string ConnectionString { get; } = ReadEnvironmentValue
    (
        key: "SHELF_API_CONNECTION_STRING",
        defaultValue: null
    );

    public static string ReadEnvironmentValue(string key, string defaultValue = null)
    {
        string value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        value = defaultValue;
        return value;
    }
}