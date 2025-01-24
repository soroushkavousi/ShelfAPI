using ShelfApi.Domain.BaseDataAggregate;

namespace ShelfApi.Application.BaseDataApplication.Interfaces;

public interface IBaseDataService
{
    FinancialSettings FinancialSettings { get; }
    Dictionary<ErrorCode, ApiError> ApiErrors { get; }

    Task InitializeAsync();

    Task LoadProjectSettingsAsync();

    Task LoadApiErrorsAsync();
}