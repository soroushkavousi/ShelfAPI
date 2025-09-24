using ShelfApi.Shared.Common.Exceptions;
using ShelfApi.Shared.Common.Extensions;

namespace ShelfApi.Shared.Common.ValueObjects;

public record Price
{
    private Price() { }

    public decimal Value { get; private set; }

    public static Price Zero { get; } = Create(0);

    public static Price Create(decimal value)
    {
        (Error error, Price price) = TryCreate(value);
        if (error is not null)
            throw new ServerException(error);

        return price;
    }

    public static Result<Price> TryCreate(decimal value)
    {
        Price price = new() { Value = value };

        if (value < 0)
            return new Error(ErrorCode.InvalidFormat, ErrorField.Price);

        return price;
    }

    public Price GetTax(decimal taxPercentage)
        => Create(Value.GetPercentage(taxPercentage));

    public static Price operator +(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = Create(p1.Value + p2.Value);
        return price;
    }

    public static Price operator -(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = Create(p1.Value - p2.Value);
        return price;
    }

    public static Price operator *(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);

        Price price = Create(p1.Value * p2.Value);
        return price;
    }

    public static Price operator /(Price p1, Price p2)
    {
        ArgumentNullException.ThrowIfNull(p1);
        ArgumentNullException.ThrowIfNull(p2);
        ArgumentOutOfRangeException.ThrowIfEqual(p2, Zero);

        Price price = Create(Math.Round(p1.Value / p2.Value));
        return price;
    }

    public static implicit operator Price(long price) => Create(price);
    public static implicit operator Price(decimal price) => Create(price);
    public static implicit operator Price(double price) => Create((decimal)price);
    public static implicit operator Price(int price) => Create(price);
    public static implicit operator Price(float price) => Create((decimal)price);
}