using IdGen;
using ShelfApi.Application.Common;
using SimpleBase;

namespace ShelfApi.Infrastructure.Tools;

public partial class IdManager : IIdManager
{
    public static readonly byte _epochLength = 41;
    public static readonly byte _serverIdLength = 6; //only 127 server instances are allowed
    public static readonly byte _sequenceLength = 16; //only 131,071 ids per millisecond
    private readonly IdGenerator _idGenerator;

    public IdManager(int serverId)
    {
        IdGeneratorOptions options = CreateOptions();
        _idGenerator = new IdGenerator(serverId, options);
    }

    private static IdGeneratorOptions CreateOptions()
    {
        IdStructure structure = new(_epochLength, _serverIdLength, _sequenceLength);
        IdGeneratorOptions options = new(structure);
        return options;
    }

    public ulong GenerateNextUlong()
    {
        ulong id; bool idIsValid;
        do
        {
            id = (ulong)_idGenerator.CreateId();
            idIsValid = IsIdValid(id);
        }
        while (!idIsValid);
        return id;
    }

    public string EncodeId(ulong id)
    {
        byte[] bytes = BitConverter.GetBytes(id);
        string encodedId = Base58.Bitcoin.Encode(bytes);
        return encodedId;
    }

    public ulong DecodeEncodedId(string encodedId)
    {
        byte[] idBytes = Base58.Bitcoin.Decode(encodedId);
        ulong id = BitConverter.ToUInt64(idBytes);
        return id;
    }

    private bool IsIdValid(ulong id)
    {
        // To filter 10-length encoded IDs
        if (id % 256 < 6)
            return false;

        try
        {
            EncodedId encodedId = new EncodedId(EncodeId(id));
        }
        catch { return false; }

        return true;
    }
}

