using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GitRate.Common.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Return collection of all assemblies referenced by <paramref name="root"/>
        /// </summary>
        public static IEnumerable<Assembly> GetAllReferencedAssemblies(this Assembly root, Func<AssemblyName, bool> condition = null)
        {
            return GetAssemblies(root, condition);
        }

        private static IEnumerable<Assembly> GetAssemblies(Assembly root, Func<AssemblyName, bool> condition = null)
        {
            var assemblies = new List<Assembly>();

            var referencedAssemblyNames = condition == null
                ? root.GetReferencedAssemblies().ToList()
                : root.GetReferencedAssemblies().Where(condition).ToList();

            foreach (var assemblyName in referencedAssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);

                assemblies.Add(assembly);

                assemblies.AddRange(GetAssemblies(assembly));
            }

            return assemblies;
        }
    }
}