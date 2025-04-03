using Elastic.Clients.Elasticsearch;
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

    public async Task<ElasticsearchResult<TDocument[]>> SearchAsync(Action<QueryDescriptor<TDocument>> searchQuery,
        int pageSize = 10, int pageNumber = 1)
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
                ),
            successFunction: response => response.Documents.ToArray(),
            pageSize, pageNumber);
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