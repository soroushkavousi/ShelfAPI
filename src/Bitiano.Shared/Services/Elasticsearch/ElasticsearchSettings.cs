namespace Bitiano.Shared.Services.Elasticsearch;

public class ElasticsearchSettings
{
    public string Url { get; init; }
    public string ApiKey { get; init; }
    public string FingerPrint { get; init; }
    public int RequestTimeout { get; init; }
    public int BulkChunkSize { get; init; } = 1000;
    public bool DebugMode { get; init; }
    public Dictionary<Type, string> IndexNames { get; init; }
}