using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Products.Elasticsearch;
using Microsoft.Extensions.Logging;

namespace Bitiano.Shared.Services.Elasticsearch;

public class ElasticsearchService<TDocument>(ILogger<ElasticsearchService<TDocument>> logger,
    ElasticsearchClient client) : IElasticsearchService<TDocument> where TDocument : class
{
    public async Task<ElasticsearchResult<bool>> AddOrUpdateAsync(TDocument document)
    {
        return await ExecuteRequestAsync(
            requestFunction: async () =>
                await client.IndexAsync(document, idx => idx.OpType(OpType.Index)),
            successFunction: response => true,
            document);
    }

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