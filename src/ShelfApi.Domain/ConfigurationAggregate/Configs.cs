using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.ConfigurationAggregate;

public class Configs : BaseModel<byte>
{
    public Configs(EnvironmentName environmentName, ConfigsCategory category, string value) : base()
    {
        EnvironmentName = environmentName;
        Category = category;
        Value = value;
    }

    static Configs()
    {
        SetDefaultConfigs();
    }

    public EnvironmentName EnvironmentName { get; }
    public ConfigsCategory Category { get; }
    public string Value { get; }

    public static JwtConfigs Jwt { get; private set; }

    public void SetAsStatic()
    {
        switch (Category)
        {
            case ConfigsCategory.JWT:
                Jwt = Value.FromJson<JwtConfigs>();
                break;
            default:
                break;
        }
    }

    private static void SetDefaultConfigs()
    {
        Jwt = new JwtConfigs("jwt-key", "https://server.com", "https://server.com");
    }
}

public record JwtConfigs(string Key, string Issuer, string Audience);

