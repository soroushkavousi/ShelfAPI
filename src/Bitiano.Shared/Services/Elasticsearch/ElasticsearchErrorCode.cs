namespace Bitiano.Shared.Services.Elasticsearch;

public enum ElasticsearchErrorCode : byte
{
    UnknownError = 1,
    InternalElasticsearchError = 2,
    BadRequest = 3,
    Unauthorized = 4,
    NotFound = 5,
    Conflict = 6
}