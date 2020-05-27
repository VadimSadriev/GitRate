using System.Reflection;
using AutoMapper;
using GitRate.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Mapping
{
    public static class Extensions
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services, Assembly rootAssembly)
        {
            var assemblies = rootAssembly.GetApplicationAssemblies();
           
            services.AddAutoMapper(assemblies);
            
            return services;
        }
    }
}