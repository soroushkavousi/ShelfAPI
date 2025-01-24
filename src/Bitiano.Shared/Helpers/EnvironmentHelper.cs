namespace Bitiano.Shared.Helpers;

public static class EnvironmentHelper
{
    public static string ReadVariable(string key, string defaultValue = null)
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