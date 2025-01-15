using Bitiano.Shared;

namespace ShelfApi.Presentation;

public static class EnvironmentVariables
{
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
}