using ShelfApi.Domain.Common.Exceptions;
using System.Text.RegularExpressions;

namespace ShelfApi.Application.Common;

public partial record EncodedId
{
    private readonly string _value;

    public EncodedId(string value)
    {
        _value = GetValidatedValue(value);
    }

    public string Value { get => _value; init => _value = GetValidatedValue(value); }

    private static string GetValidatedValue(string encodedId)
    {
        if (!IsValueValid(encodedId))
            throw new ServerException(ErrorCode.InvalidFormat, encodedId, ErrorField.EncodedId);

        return encodedId;
    }

    private static bool IsValueValid(string encodedId)
    {
        if (string.IsNullOrWhiteSpace(encodedId))
            return false;

        encodedId = encodedId.Trim();
        if (encodedId.Length != 11)
            return false;

        // To filter encoded IDs which has three same characters in a row
        if (_encodedIdWrongRepetitionRegex.IsMatch(encodedId))
            return false;

        if (_encodedIdWrongCharactersRegex.IsMatch(encodedId))
            return false;

        if (_encodedIdValidCharactersRegex.IsMatch(encodedId) == false)
            return false;

        return true;
    }

    private static readonly Regex _encodedIdWrongRepetitionRegex = EncodedIdWrongRepetitionRegex();
    private static readonly Regex _encodedIdWrongCharactersRegex = EncodedIdWrongCharactersRegex();
    private static readonly Regex _encodedIdValidCharactersRegex = EncodedIdValidCharactersRegex();

    [GeneratedRegex("(\\S)\\1{2}")]
    private static partial Regex EncodedIdWrongRepetitionRegex();

    [GeneratedRegex("([0OlI])")]
    private static partial Regex EncodedIdWrongCharactersRegex();

    [GeneratedRegex("^[a-zA-Z0-9]+$")]
    private static partial Regex EncodedIdValidCharactersRegex();
}