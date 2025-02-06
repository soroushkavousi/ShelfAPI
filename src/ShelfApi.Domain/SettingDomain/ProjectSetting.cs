using ShelfApi.Domain.Common.Model;

namespace ShelfApi.Domain.SettingDomain;

public class ProjectSetting : BaseModel
{
    private ProjectSetting() { }

    public string Key { get; }
    public string Value { get; }
}