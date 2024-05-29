using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.BaseDataAggregate;

public class MainSettings : BaseModel
{
    private MainSettings()
    { }

    public MainSettings(MainSettingsCategory category, string data) : base()
    {
        Category = category;
        Data = data;
    }

    public MainSettingsCategory Category { get; }
    public string Data { get; }
}