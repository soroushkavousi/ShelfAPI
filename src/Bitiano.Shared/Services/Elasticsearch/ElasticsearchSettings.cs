using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;

namespace Bitiano.Shared.Services.Elasticsearch;

public class ElasticsearchSettings
{
    public string Url { get; init; }
    public string ApiKey { get; init; }
    public string FingerPrint { get; init; }
    public int RequestTimeout { get; init; }

    [JsonIgnore]
    public Dictionary<Type, Action<ClrTypeMappingDescriptor>> DefaultMappings { get; init; } = new();
}