using System.Text.Json.Serialization.Metadata;

namespace Bitiano.Shared.Tools.Serializer.Modifiers;

[AttributeUsage(AttributeTargets.Property)]
public class SensitiveAttribute : Attribute;

public static class SensitiveModifier
{
    public static void Modify(JsonTypeInfo typeInfo)
    {
        foreach (JsonPropertyInfo propertyInfo in typeInfo.Properties)
        {
            object[] serializationCountAttributes = propertyInfo.AttributeProvider?
                .GetCustomAttributes(typeof(SensitiveAttribute), true) ?? [];

            SensitiveAttribute attribute = serializationCountAttributes.Length == 1
                ? (SensitiveAttribute)serializationCountAttributes[0]
                : null;

            if (attribute == null)
                continue;

            Func<object, object> getProperty = propertyInfo.Get;
            if (getProperty is not null)
            {
                propertyInfo.Get = _ =>
                {
                    string maskedValue = $"(SENSITIVE_{propertyInfo.PropertyType.Name.ToUpper()})";
                    return maskedValue;
                };
            }
        }
    }
}