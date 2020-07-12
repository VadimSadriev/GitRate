using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extensions methods for <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Return first generic method of given name
        /// </summary>
        public static MethodInfo GetGenericMethod(this Type type, string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException(nameof(methodName), "Method name cannot be null or empty");

            var method = type
                         .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                         .FirstOrDefault(x => x.Name == methodName && x.ContainsGenericParameters);

            return method;
        }
    }
}
