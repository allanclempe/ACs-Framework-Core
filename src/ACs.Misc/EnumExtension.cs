using System;

namespace ACs.Misc
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum obj)
        {
            return GetDescription(obj, null);
        }

        public static string GetDescription(this Enum obj, string[] parameters)
        {
            var message = EnumStringValueAttribute.GetStringValue(obj);
            return parameters != null ? string.Format(message, parameters) : message;
        }
    }
}
