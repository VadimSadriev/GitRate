using System.Reflection;
using System.Text.Json.Serialization;
using GitRate.Common.Extensions;
using GitRate.Common.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Mvc
{
    /// <summary>
    /// Extensions for Mvc configuration
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds configured mvc to aspnet project
        /// </summary>
        public static IMvcBuilder AddCustomMvc(this IServiceCollection services, IConfiguration configuration, Assembly rootAssembly)
        {
            return services.AddControllers(options =>
                {
                    var filters = rootAssembly.GetTypesAssignableFromAssemblyDependencies<ICustomActionFilter>();

                    foreach (var filter in filters)
                        options.Filters.Add(filter);
                })
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
        }
    }
}