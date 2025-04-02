namespace Bitiano.Shared.Services.Elasticsearch;

public interface IElasticsearchService<TDocument> where TDocument : class
{
    Task<ElasticsearchResult<bool>> AddOrUpdateAsync(TDocument document);
}