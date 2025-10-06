using ShelfApi.Shared.Common.ValueObjects;

namespace ShelfApi.SettingModule.Domain;

public class ProjectSetting : DomainModel
{
    private ProjectSetting() { }

    public string Key { get; }
    public string Value { get; }
}