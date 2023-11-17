namespace ShelfApi.Domain.Common.Extensions;

public static class EnumExtensions
{
    public static TEnum? ToEnum<TEnum>(this string enumString) where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(enumString))
            return default;

        if (!Enum.TryParse(enumString, true, out TEnum @enum))
            return default;

        return @enum;
    }
}
