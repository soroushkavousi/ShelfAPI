using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Bitiano.Shared.Services.Elasticsearch;

public interface IElasticsearchService<TDocument> where TDocument : class
{
    Task<ElasticsearchResult<bool>> AddOrUpdateAsync(TDocument document);

    Task<ElasticsearchResult<TDocument[]>> SearchAsync(Action<QueryDescriptor<TDocument>> searchQuery,
        Action<SortOptionsDescriptor<TDocument>> sort, int pageNumber = 1, int pageSize = 10);

    Task<ElasticsearchResult<bool>> BulkAddOrUpdateAsync(IEnumerable<TDocument> documents);
}