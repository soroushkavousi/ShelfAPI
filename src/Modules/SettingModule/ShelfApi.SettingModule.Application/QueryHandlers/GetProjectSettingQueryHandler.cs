using MediatR;
using Microsoft.EntityFrameworkCore;
using ShelfApi.SettingModule.Application.Interfaces;
using ShelfApi.SettingModule.Contracts.Queries;
using ShelfApi.Shared.Common.Exceptions;

namespace ShelfApi.SettingModule.Application.QueryHandlers;

public class GetProjectSettingQueryHandler(ISettingDbContext settingDbContext)
    : IRequestHandler<GetProjectSettingQuery, string>
{
    public async Task<string> Handle(GetProjectSettingQuery request, CancellationToken cancellationToken)
    {
        string projectSettingJson = await settingDbContext.ProjectSettings
            .Where(x => x.Key == request.Key)
            .Select(x => x.Value)
            .FirstOrDefaultAsync(cancellationToken);

        if (projectSettingJson is null)
            throw new ServerException("Could not fetch project setting");

        return projectSettingJson;
    }
}