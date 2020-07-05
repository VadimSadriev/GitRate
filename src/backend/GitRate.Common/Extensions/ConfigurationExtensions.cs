using System;
using Microsoft.Extensions.Configuration;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IConfiguration"/> and <see cref="IConfigurationSection"/>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Checks if  current section is exists
        /// </summary>
        public static IConfigurationSection CheckExistence(this IConfigurationSection section)
        {
            if (!section.Exists())
                throw new ArgumentNullException($"Configuration {section.Path} not found");

            return section;
        }
    }
}