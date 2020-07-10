using System.Reflection;
using AutoMapper;
using GitRate.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Mapping
{
    /// <summary>
    /// Extensions for <see cref="AutoMapper"/>
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds mapping profiles from root and related assemblies to Di to map with <see cref="IMapper"/>
        /// </summary>
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services, Assembly rootAssembly)
        {
            var assemblies = rootAssembly.GetReferencedApplicationAssemblies();
           
            services.AddAutoMapper(assemblies);
            
            return services;
        }
    }
}