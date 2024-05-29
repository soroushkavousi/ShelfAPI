namespace ShelfApi.Domain.BaseDataAggregate;

public record JwtSettings(string Key, string Issuer, string Audience);
public record FinancialSettings(decimal TaxPercentage);