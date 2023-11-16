namespace ShelfApi.Application.Common;

public interface IIdManager
{
    public ulong GenerateNextUlong();
    public string EncodeId(ulong id);
    public ulong DecodeEncodedId(string encodedId);
}

