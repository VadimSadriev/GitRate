using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Mvc
{
    public static class Extensions
    {
        public static IMvcBuilder AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
           return services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
        }
    }
}