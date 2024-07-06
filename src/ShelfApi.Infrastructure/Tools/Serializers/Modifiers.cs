using ShelfApi.Domain.Common;
using System.Text.Json.Serialization.Metadata;

namespace ShelfApi.Infrastructure.Tools;

public class Modifiers
{
    public static void SensitiveModifier(JsonTypeInfo typeInfo)
    {
        foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
        {
            object[] serializationCountAttributes = propertyInfo.AttributeProvider?.GetCustomAttributes(typeof(SensitiveAttribute), true) ?? Array.Empty<object>();
            SensitiveAttribute attribute = serializationCountAttributes.Length == 1 ? (SensitiveAttribute)serializationCountAttributes[0] : null;

            if (attribute != null)
            {
                var getProperty = propertyInfo.Get;
                if (getProperty is not null)
                {
                    propertyInfo.Get = (obj) =>
                    {
                        var maskedValue = $"(SENSITIVE_{propertyInfo.PropertyType.Name.ToUpper()})";
                        return maskedValue;
                    };
                }
            }
        }
    }
}
