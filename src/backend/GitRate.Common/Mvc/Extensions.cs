using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace GitRate.Common.Mvc
{
    public static class Extensions
    {
        public static IMvcBuilder AddCustomMvc(this IServiceCollection services)
        {
            return services.AddControllers()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 });
        }
    }
}
