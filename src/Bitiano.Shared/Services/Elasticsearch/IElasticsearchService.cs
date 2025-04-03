using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Bitiano.Shared.Services.Elasticsearch;

public interface IElasticsearchService<TDocument> where TDocument : class
{
    Task<ElasticsearchResult<bool>> AddOrUpdateAsync(TDocument document);

    Task<ElasticsearchResult<TDocument[]>> SearchAsync(Action<QueryDescriptor<TDocument>> searchQuery,
        int pageSize = 10, int pageNumber = 1);
}