using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.DependencyInjection;
using ApiKey = Elastic.Transport.ApiKey;

namespace Bitiano.Shared.Services.Elasticsearch;

public static class ServiceInjector
{
    public static void AddElasticsearch(this IServiceCollection services, ElasticsearchSettings settings)
    {
        NodePool nodePool = new SingleNodePool(new(settings.Url));
        ElasticsearchClientSettings clientSettings = new ElasticsearchClientSettings(nodePool)
            .Authentication(new ApiKey(settings.ApiKey))
            .CertificateFingerprint(settings.FingerPrint)
            .RequestTimeout(TimeSpan.FromSeconds(settings.RequestTimeout));

        foreach ((Type type, Action<ClrTypeMappingDescriptor> mapping) in settings.DefaultMappings)
        {
            clientSettings.DefaultMappingFor(type, mapping);
        }

        ElasticsearchClient client = new(clientSettings);
        services.AddSingleton(client);
        services.AddScoped(typeof(IElasticsearchService<>), typeof(ElasticsearchService<>));
    }
}