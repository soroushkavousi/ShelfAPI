using MediatR;
using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.SettingModule.Contracts.Queries;

public class GetProjectSettingQuery : IRequest<string>
{
    public string Key { get; init; }
}