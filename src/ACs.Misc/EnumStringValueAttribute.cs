using System;
using System.Reflection;
namespace ACs.Misc
{
    public class EnumStringValueAttribute : Attribute
    {
        public EnumStringValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
        public string Label { get; set; }

        public static string GetStringValue(Enum value)
        {
            var type = value.GetType().GetTypeInfo();
            var fieldInfo = type.GetField(value.ToString());

            var attribs = fieldInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];

            return attribs != null && attribs.Length > 0 ? attribs[0].Value : null;
        }

        public static string GetStringLabel(Enum value)
        {
            var type = value.GetType().GetTypeInfo();
            var fieldInfo = type.GetField(value.ToString());

            var attribs = fieldInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];

            return attribs != null && attribs.Length > 0 ? attribs[0].Label : null;
        }


        public static object GetStringValue(string stringValue, Type enumType)
        {
            var fieldsInfo = enumType.GetTypeInfo().GetFields();

            foreach (var fi in fieldsInfo)
            {
                var attribs = fi.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];

                if (attribs != null && attribs.Length > 0 && attribs[0].Value == stringValue)
                    return Enum.Parse(enumType, fi.Name);
            }

            return null;
        }
    }
}
