using ShelfApi.Application.Common;
using SimpleBase;

namespace ShelfApi.Infrastructure.Tools;

public partial class IdManager : IIdManager
{
    public string EncodeId(long id)
    {
        byte[] bytes = BitConverter.GetBytes(id);
        string encodedId = Base58.Bitcoin.Encode(bytes);
        return encodedId;
    }

    public long DecodeEncodedId(string encodedId)
    {
        byte[] idBytes = Base58.Bitcoin.Decode(encodedId);
        long id = BitConverter.ToInt64(idBytes);
        return id;
    }
}