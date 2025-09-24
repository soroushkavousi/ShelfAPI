namespace ShelfApi.Shared.Common.Tools;

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

    public static int ReadIntVariable(string key, int defaultValue = 0)
    {
        string value = ReadVariable(key, defaultValue.ToString());
        return int.Parse(value);
    }
}