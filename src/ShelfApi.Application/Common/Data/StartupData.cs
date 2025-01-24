namespace ShelfApi.Application.Common.Data;

public record StartupData
{
    public static readonly StartupData Default = new()
    {
        DbConnectionString = "Host=localhost;Port=5432;Database=shelf;Username=postgres;Password=postgres",
        JwtKey = "a-random-key-hj4E@vE724fdZrli.SB2#4yeJfD55v24ErK2XkdIBSa1f7asf14V",
        JwtIssuer = "http://localhost",
        JwtAudience = "http://localhost"
    };

    public string DbConnectionString { get; set; }
    public string JwtKey { get; init; }
    public string JwtIssuer { get; init; }
    public string JwtAudience { get; init; }
}