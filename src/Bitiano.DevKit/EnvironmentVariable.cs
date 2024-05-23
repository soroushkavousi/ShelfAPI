namespace Bitiano.DevKit;

public class EnvironmentVariable
{
    public string Key { get; }
    public string Value { get; private set; }
    public string Default { get; }

    public EnvironmentVariable(string key, string @default)
    {
        Key = key;
        Default = @default;
        ReadValue();
    }

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