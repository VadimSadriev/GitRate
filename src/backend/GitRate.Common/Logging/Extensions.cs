using GitRate.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GitRate.Common.Logging
{
    /// <summary>
    /// Extensions for logging
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds serilog to Di
        /// </summary>
        public static IServiceCollection AddCustomLogging(this IServiceCollection services, IConfiguration configuration)
        {
            var loggingSection = configuration.GetSection("Logging").CheckExistence();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConfiguration(loggingSection);
                builder.AddSerilog(
                    new LoggerConfiguration().ReadFrom.Configuration(loggingSection)
                        .CreateLogger());
            });

            return services;
        }
    }
}