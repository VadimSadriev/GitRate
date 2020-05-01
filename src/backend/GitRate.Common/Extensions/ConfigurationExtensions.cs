using System;
using Microsoft.Extensions.Configuration;

namespace GitRate.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection CheckExistence(this IConfigurationSection section)
        {
            if (!section.Exists())
                throw new ArgumentNullException($"Configuration {section.Path} not found");

            return section;
        }
    }
}