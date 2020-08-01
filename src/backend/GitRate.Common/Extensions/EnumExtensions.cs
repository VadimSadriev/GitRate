using System;
using System.Collections.Generic;
using System.Linq;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extensions for enums
    /// </summary>
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum @enum)
        {
            var enumType = @enum.GetType();

            var fieldName = Enum.GetName(enumType, @enum);

            return enumType.GetField(fieldName)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        /// <summary>
        /// Returns values of type <typeparamref name="TEnum"/>
        /// </summary>
        public static IEnumerable<TEnum> GetValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
    }
}
