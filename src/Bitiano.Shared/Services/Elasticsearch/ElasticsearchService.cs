using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport.Products.Elasticsearch;
using Microsoft.Extensions.Logging;

namespace Bitiano.Shared.Services.Elasticsearch;

public class ElasticsearchService<TDocument>(ILogger<ElasticsearchService<TDocument>> logger,
    ElasticsearchClient client, ElasticsearchSettings settings)
    : IElasticsearchService<TDocument> where TDocument : class
{
    public async Task<ElasticsearchResult<bool>> AddOrUpdateAsync(TDocument document)
    {
        return await ExecuteRequestAsync(
            requestFunction: async () =>
                await client.IndexAsync(document, idx => idx.OpType(OpType.Index)),
            successFunction: response => true,
            document);
    }

    public async Task<ElasticsearchResult<bool>> BulkAddOrUpdateAsync(IEnumerable<TDocument> documents)
    {
        string indexName = settings.IndexNames[typeof(TDocument)];

        foreach (TDocument[] chunk in documents.Chunk(settings.BulkChunkSize))
        {
            List<IBulkOperation> operations = chunk
                .Select(document => new BulkIndexOperation<TDocument>(document) { Document = document })
                .Cast<IBulkOperation>()
                .ToList();

            BulkRequest request = new(indexName)
            {
                Operations = operations
            };

            (ElasticsearchErrorCode? errorCode, bool success) = await ExecuteRequestAsync(
                requestFunction: async () =>
                    await client.BulkAsync(request),
                successFunction: response => !response.Errors,
                chunk.Length);

            if (errorCode.HasValue)
                return errorCode;

            if (!success)
                return false;
        }

        return true;
    }

    public async Task<ElasticsearchResult<TDocument[]>> SearchAsync(Action<QueryDescriptor<TDocument>> searchQuery,
        Action<SortOptionsDescriptor<TDocument>> sort, int pageNumber = 1, int pageSize = 10)
    {
        string indexName = settings.IndexNames[typeof(TDocument)];

        int from = CalculatePageFromOffset(pageNumber, pageSize);

        return await ExecuteRequestAsync(
            requestFunction: async () =>
                await client.SearchAsync<TDocument>(x => x
                    .Index(indexName)
                    .From(from)
                    .Size(pageSize)
                    .Query(searchQuery)
                    .Sort(sort)
                ),
            successFunction: response => response.Documents.ToArray(),
            pageNumber, pageSize);
    }

    private static int CalculatePageFromOffset(int pageNumber, int pageSize)
        => (pageNumber - 1) * pageSize;

    private async Task<ElasticsearchResult<TData>> ExecuteRequestAsync<TResponse, TData>(
        Func<Task<TResponse>> requestFunction,
        Func<TResponse, TData> successFunction,
        params object[] inputs) where TResponse : ElasticsearchResponse
    {
        try
        {
            TResponse response = await requestFunction();
            if (response.IsValidResponse)
                return successFunction(response);

            ElasticsearchErrorCode errorCode = response.ApiCallDetails.HttpStatusCode switch
            {
                400 => ElasticsearchErrorCode.BadRequest,
                401 => ElasticsearchErrorCode.Unauthorized,
                404 => ElasticsearchErrorCode.NotFound,
                409 => ElasticsearchErrorCode.Conflict,
                500 => ElasticsearchErrorCode.InternalElasticsearchError,
                _ => ElasticsearchErrorCode.UnknownError
            };

            logger.LogError("Elasticsearch request failed" +
                " - error: {ElasticsearchError}" +
                " - statusCode: {ElasticsearchStatusCode}" +
                " - errorCode: {ReturnedErrorCode}" +
                " - inputs: {@inputs}",
                response.ElasticsearchServerError?.Error.ToString(),
                response.ApiCallDetails.HttpStatusCode,
                errorCode,
                inputs);

            return errorCode;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during Elasticsearch request" +
                " - exceptionMessage: {ExceptionMessage}" +
                " - inputs: {@Inputs}"
                , ex.Message
                , inputs);
            return ElasticsearchErrorCode.UnknownError;
        }
    }
}