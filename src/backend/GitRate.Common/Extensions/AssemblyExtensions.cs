using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GitRate.Common.Extensions
{
    public static class AssemblyExtensions
    {
        private static readonly Func<AssemblyName, bool> ApplicationAssembliesCondition = assemblyName => assemblyName.FullName.StartsWith("GitRate");
        
        /// <summary>
        /// Return types from assembly dependencies which assignable from <typeparamref name="TParent"/>
        /// </summary>
        public static IEnumerable<Type> GetTypesAssignableFromAssemblyDependencies<TParent>(this Assembly root)
        {
            var referencedAssemblies = root.GetApplicationAssemblies();

            var parentType = typeof(TParent);

            var result = new List<Type>();
            
            foreach (var referencedAssembly in referencedAssemblies)
            {
                var types = referencedAssembly.GetTypes().Where(type => parentType.IsAssignableFrom(type) && parentType != type).ToArray();
                
                result.AddRange(types);
            }

            return result;
        }
        
        /// <summary>
        /// Return collection of all assemblies referenced by <paramref name="root"/>
        /// </summary>
        public static IEnumerable<Assembly> GetApplicationAssemblies(this Assembly root)
        {
            return GetReferencedAssembliesInternal(root).Distinct();
        }

        private static IEnumerable<Assembly> GetReferencedAssembliesInternal(Assembly root)
        {
            var assemblies = new List<Assembly>();

            var referencedAssemblyNames = root.GetReferencedAssemblies().Where(ApplicationAssembliesCondition).ToList();

            foreach (var assemblyName in referencedAssemblyNames)
            {
                var assembly = Assembly.Load(assemblyName);

                assemblies.Add(assembly);

                assemblies.AddRange(GetReferencedAssembliesInternal(assembly));
            }

            return assemblies;
        }
    }
}