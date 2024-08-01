namespace ShelfApi.Application.Common;

public interface IIdManager
{
    string EncodeId(long id);

    long DecodeEncodedId(string encodedId);
}