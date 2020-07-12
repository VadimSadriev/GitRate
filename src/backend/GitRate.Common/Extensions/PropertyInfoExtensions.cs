using System.Collections;
using System.Reflection;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="PropertyInfo"/>
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Returns true if type of object under property is IEnumerable and not a string
        /// </summary>
        public static bool IsIEnumerable(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return false;

            return propertyInfo.PropertyType != typeof(string)
                && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
        }

        /// <summary>
        /// Returns true if type of ojbect under property is class and not a string
        /// </summary>
        public static bool IsClass(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                return false;

            return propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string);
        }
    }
}
