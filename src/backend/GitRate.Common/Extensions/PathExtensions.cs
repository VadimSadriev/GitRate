using System;
using System.IO;
using System.Reflection;

namespace GitRate.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Path"/>
    /// </summary>
    public static class PathExtensions
    {
        /// <summary>
        /// Returns directory of current execution code
        /// </summary>
        public static string? GetExecutionDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}