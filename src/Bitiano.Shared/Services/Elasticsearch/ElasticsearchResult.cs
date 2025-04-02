using Bitiano.Shared.ValueObjects;

namespace Bitiano.Shared.Services.Elasticsearch;

public record ElasticsearchResult<TData> : BaseResult<TData, ElasticsearchErrorCode>
{
    public ElasticsearchResult(TData data) : base(data) { }

    public ElasticsearchResult(ElasticsearchErrorCode error) : base(error) { }

    public static implicit operator ElasticsearchResult<TData>(ElasticsearchErrorCode errorCode) => new(errorCode);
    public static implicit operator ElasticsearchResult<TData>(TData data) => new(data);
}