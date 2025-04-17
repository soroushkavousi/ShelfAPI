using ShelfApi.Domain.Common.Model;

namespace ShelfApi.Domain.SettingDomain;

public class ProjectSetting : DomainModel
{
    private ProjectSetting() { }

    public string Key { get; }
    public string Value { get; }
}