using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.Domain.SettingDomain;

public class ProjectSetting : DomainModel
{
    private ProjectSetting() { }

    public string Key { get; }
    public string Value { get; }
}