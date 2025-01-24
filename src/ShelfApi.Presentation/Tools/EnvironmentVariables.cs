using Bitiano.Shared.Helpers;

namespace ShelfApi.Presentation.Tools;

public static class EnvironmentVariables
{
    public static string ConnectionString { get; } = EnvironmentHelper.ReadVariable
    (
        key: "SHELF_API_CONNECTION_STRING",
        defaultValue: null
    );
}