using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace ShelfApi.Infrastructure.Tools.Serializers.NamingPolicies;

public class KebabCaseNamingPolicy : JsonNamingPolicy
{
    private readonly SnakeCaseNamingStrategy _newtonsoftSnakeCaseNamingStrategy = new();

    public static KebabCaseNamingPolicy Instance { get; } = new KebabCaseNamingPolicy();

    public override string ConvertName(string name)
    {
        return _newtonsoftSnakeCaseNamingStrategy.GetPropertyName(name, false).Replace("_", "-");
    }
}