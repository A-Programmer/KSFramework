using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KSFramework.Utilities;
public static class EnumExtensions
{
    public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        return Enum.GetValues(input.GetType()).Cast<T>();
    }

    public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        foreach (var value in Enum.GetValues(input.GetType()))
            if ((input as Enum)!.HasFlag(flag: value as Enum ?? throw new InvalidOperationException()))
                yield return (T)value;
    }

    public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
    {
        Assert.NotNull(value, nameof(value));

        var attribute = (value.GetType().GetField(value.ToString()) ?? throw new InvalidOperationException())
            .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

        if (attribute == null)
            return value.ToString();

        var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
        return propValue.ToString();
    }

    public static Dictionary<int, string> ToDictionary(this Enum value)
    {
        return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
    }

    public static Dictionary<int, string> ToDescriptionDictionary(this Enum value)
    {
        return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
    }
    
    public static Dictionary<string, string> GetEnumDescriptions<TEnum>() where TEnum : struct, Enum
    {
        var dict = new Dictionary<string, string>();
        foreach (var value in Enum.GetValues<TEnum>())
        {
            var field = typeof(TEnum).GetField(value.ToString());
            var descAttr = field?.GetCustomAttribute<DescriptionAttribute>();
            dict.Add(value.ToString(), descAttr?.Description ?? value.ToString());
        }
        return dict;
    }

    public static Dictionary<int, string> ToDictionary<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                value => Convert.ToInt32(value),
                value => value.GetDescription()
            );
    }
    
    private static string GetDescription<TEnum>(this TEnum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}

public enum DisplayProperty
{
    Description,
    GroupName,
    Name,
    Prompt,
    ShortName,
    Order
}