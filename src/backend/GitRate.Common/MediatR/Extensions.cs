using System.Linq;
using System.Reflection;
using GitRate.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.MediatR
{
    public static class Extensions
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly rootAssembly)
        {
            var assemblies = rootAssembly.GetApplicationAssemblies();

            services.AddMediatR(assemblies.ToArray());

            return services;
        }

      
    }
}