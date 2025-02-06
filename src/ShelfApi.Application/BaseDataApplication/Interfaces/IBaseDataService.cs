namespace ShelfApi.Application.BaseDataApplication.Interfaces;

public interface IBaseDataService
{
    Dictionary<ErrorCode, ApiError> ApiErrors { get; }

    Task InitializeAsync();

    Task LoadApiErrorsAsync();
}