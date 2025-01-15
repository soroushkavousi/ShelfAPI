using ShelfApi.Domain.Common.Model;

namespace ShelfApi.Domain.BaseDataAggregate;

public class ProjectSetting : BaseModel
{
    private ProjectSetting()
    {
    }

    public ProjectSetting(ProjectSettingId id, string name, string data)
    {
        Id = id;
        Name = name;
        Data = data;
    }

    public ProjectSettingId Id { get; }
    public string Name { get; }
    public string Data { get; }
}