using System.ComponentModel;

namespace Service.Helpers;

public class EnumHelper
{
    public static IDictionary<int, string> GetEnumDictionary<T>()
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        var dictionary = new Dictionary<int, string>();
        foreach (var value in Enum.GetValues(enumType))
        {
            var name = Enum.GetName(enumType, value);
            if (name != null)
            {
                var field = enumType.GetField(name);
                if (field != null)
                {
                    var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    dictionary.Add((int)value, attr?.Description ?? name);
                }
            }
        }

        return dictionary;
    }
}