using ShelfApi.Shared.Common.Tools;

namespace ShelfApi.Presentation.Tools;

public static class EnvironmentVariables
{
    public static string ConnectionString { get; } = EnvironmentHelper.ReadVariable
    (
        key: "SHELF_API_CONNECTION_STRING",
        defaultValue: null
    );

    public static int InstanceId { get; } = EnvironmentHelper.ReadIntVariable
    (
        key: "SHELF_API_INSTANCE_ID",
        defaultValue: 1
    );
}