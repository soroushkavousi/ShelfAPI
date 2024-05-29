using ShelfApi.Domain.BaseDataAggregate;

namespace ShelfApi.Application.BaseDataApplication.Interfaces;

public interface IBaseDataService
{
    JwtSettings JwtSettings { get; }
    FinancialSettings FinancialSettings { get; }

    Task InitializeAsync(IShelfApiDbContext shelfApiDbContext);
}