using ShelfApi.Domain.Common.Extensions;

namespace ShelfApi.Domain.FinancialAggregate;

public record Price
{
    public Price(decimal value)
    {
        Value = value;
        Validate();
    }

    public decimal Value { get; private set; }

    public static Price Zero => new(0);

    private void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(Value, 0);
    }

    public Price GetTax(decimal taxPercentage)
        => new(Value.GetPercentage(taxPercentage));

    public static Price operator +(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = new(p1.Value + p2.Value);
        return price;
    }

    public static Price operator -(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = new(p1.Value - p2.Value);
        return price;
    }

    public static Price operator *(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = new(p1.Value * p2.Value);
        return price;
    }

    public static Price operator /(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);
        ArgumentOutOfRangeException.ThrowIfEqual(p2, Zero);

        Price price = new(Math.Round(p1.Value / p2.Value));
        return price;
    }
}