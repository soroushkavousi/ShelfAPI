using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.ConfigurationAggregate;

public class Configs : BaseModel<byte>
{
    public static Configs Current { get; set; } = new
    (
        id: 1,
        jwt: new JwtConfigs("temp-key", "http://localhost:57074", "http://localhost:57074")
    );

    public Configs(byte id, JwtConfigs jwt) : base(id)
    {
        Jwt = jwt;
    }

    public JwtConfigs Jwt { get; init; }

    public record JwtConfigs(string Key, string Issuer, string Audience);
}
