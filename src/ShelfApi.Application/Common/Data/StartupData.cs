using Bitiano.Shared.Tools.Serializer;
using ShelfApi.Application.ProductApplication.Models.Dtos.Elasticsearch;

namespace ShelfApi.Application.Common.Data;

public record StartupData
{
    public static readonly StartupData Default = new()
    {
        DbConnectionString = "Host=localhost;Port=5432;Database=shelf;Username=postgres;Password=postgres",
        Jwt = new()
        {
            Key = "a-random-key-hj4E@vE724fdZrli.SB2#4yeJfD55v24ErK2XkdIBSa1f7asf14V",
            Issuer = "http://localhost",
            Audience = "http://localhost"
        },
        Redis = new()
        {
            Configuration = "localhost:6379",
            InstanceName = "tmp"
        },
        Elasticsearch = new()
        {
            Url = "https://localhost:9200",
            ApiKey = "the-api-key",
            FingerPrint = "the-finger-print",
            RequestTimeout = 20,
            DebugMode = true,
            IndexNames = new()
            {
                [nameof(ProductElasticDocument).ToCamelCase()] = "products"
            }
        }
    };

    public string DbConnectionString { get; set; }
    public JwtStartupData Jwt { get; init; }
    public RedisStartupData Redis { get; init; }
    public ElasticsearchStartupData Elasticsearch { get; init; }
}

public record JwtStartupData
{
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
}

public record RedisStartupData
{
    public string Configuration { get; init; }
    public string InstanceName { get; init; }
}

public record ElasticsearchStartupData
{
    private readonly Dictionary<string, string> _indexNames;

    public string Url { get; init; }
    public string ApiKey { get; init; }
    public string FingerPrint { get; init; }
    public int RequestTimeout { get; init; }
    public bool DebugMode { get; init; }

    public Dictionary<string, string> IndexNames
    {
        get => _indexNames;
        init => _indexNames = new(value, StringComparer.OrdinalIgnoreCase);
    }
}