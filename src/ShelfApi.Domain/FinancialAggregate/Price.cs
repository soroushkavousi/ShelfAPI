using ShelfApi.Domain.Common.Extensions;
using ShelfApi.Domain.ConfigurationAggregate;

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
        if (Value < 0)
            throw new ArgumentOutOfRangeException(nameof(Value), "price value should not be negative");
    }

    public Price GetTaxPrice()
        => new(Value.GetPercentage(Configs.Financial.TaxPercentage));

    public static Price operator +(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        Price price = new(p1.Value + p2.Value);
        return price;
    }

    public static Price operator -(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        Price price = new(p1.Value - p2.Value);
        return price;
    }

    public static Price operator *(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        Price price = new(p1.Value * p2.Value);
        return price;
    }

    public static Price operator /(Price p1, Price p2)
    {
        if (p1 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 is null)
            throw new ArgumentNullException(nameof(p1));

        if (p2 == Zero)
            throw new ArgumentOutOfRangeException(nameof(p1));

        Price price = new(Math.Round(p1.Value / p2.Value));
        return price;
    }
}