using System.Linq;
using System.Reflection;
using GitRate.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.MediatR
{
    /// <summary>
    /// Extensions for <see cref="Mediator"/>
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds mediator handlers and other mediator stuff to Di from root and related assemblies
        /// </summary>
        public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly rootAssembly)
        {
            var assemblies = rootAssembly.GetReferencedApplicationAssemblies();

            services.AddMediatR(assemblies.ToArray());

            return services;
        }

      
    }
}