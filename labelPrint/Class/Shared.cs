using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace labelPrint.Class
{
    public enum AppointmentStatus
    {
        [Description("#89E874:SUCCESS boxHelp")] // green - 0
        Success = 0,
        [Description("#FF8000:PROGRESS boxHelp")] // orange - 1
        Progress,
        [Description("#E67E73:UNSUCCESS boxHelp")] // red - 2
        Unsuccess,
        [Description("#0FB3FF:FUTURE boxHelp")] // blue - 3
        Future,
        [Description("#FFCC00:WARNING boxHelp")] // Yellow - 4
        Warning

    }

    public static class Enums
    {
        /// Get all values
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// Get all the names
        public static IEnumerable<T> GetNames<T>()
        {
            return Enum.GetNames(typeof(T)).Cast<T>();
        }

        /// Get the name for the enum value
        public static string GetName<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        /// Get the underlying value for the Enum string
        public static int GetValue<T>(string enumString)
        {
            return (int)Enum.Parse(typeof(T), enumString.Trim());
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }
}