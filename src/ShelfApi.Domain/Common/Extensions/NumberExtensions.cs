namespace ShelfApi.Domain.Common.Extensions;

public static class NumberExtensions
{
    public static decimal GetPercentage(this decimal number, decimal percentage)
        => number * percentage / 100;

    public static decimal GetRemainingPercentage(this decimal number, decimal percentage)
        => number.GetPercentage(100 - percentage);
}
