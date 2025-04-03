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

        if (settings.DebugMode)
            clientSettings.EnableDebugMode(x =>
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}]" +
                    $" Elasticsearch debug information: \n{x.DebugInformation}");
            });

        foreach ((Type type, string indexName) in settings.IndexNames)
        {
            clientSettings.DefaultMappingFor(type, x => x.IndexName(indexName));
        }

        ElasticsearchClient client = new(clientSettings);
        services.AddSingleton(client);
        services.AddSingleton(settings);
        services.AddScoped(typeof(IElasticsearchService<>), typeof(ElasticsearchService<>));
    }
}