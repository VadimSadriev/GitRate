using System;
using System.IO;
using System.Reflection;

namespace GitRate.Common.Extensions
{
    public static class PathExtensions
    {
        public static string GetExecutionDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}