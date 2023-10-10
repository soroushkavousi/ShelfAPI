namespace ShelfApi.Presentation.Tools;

public class EnvironmentVariable
{
    public string Key { get; private set; }
    public string Value { get; private set; }
    public string Default { get; private set; }

    private EnvironmentVariable(string key, string @default)
    {
        Key = key;
        Default = @default;
        ReadValue();
    }

    public static EnvironmentVariable EnvironmentName { get; } = new
    (
        key: "SHELF_API_ASPNETCORE_ENVIRONMENT",
        @default: "Production"
    );

    public static EnvironmentVariable ConnectionString { get; } = new
    (
        key: "SHELF_API_CONNECTION_STRING",
        @default: null
    );

    private void ReadValue()
    {
        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.Process);
        if (!string.IsNullOrWhiteSpace(Value))
            return;

        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.Machine);
        if (!string.IsNullOrWhiteSpace(Value))
            return;

        Value = Environment.GetEnvironmentVariable(Key, EnvironmentVariableTarget.User);
        if (!string.IsNullOrWhiteSpace(Value))
            return;

        Value = Default;
    }
}

