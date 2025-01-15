namespace ShelfApi.Application.Common.Tools;

public interface IIdManager
{
    string EncodeId(long id);

    long DecodeEncodedId(string encodedId);
}