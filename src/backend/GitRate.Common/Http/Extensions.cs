using GitRate.Common.Extensions;
using GitRate.Common.Http.Clients.Github;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GitRate.Common.Http
{
    /// <summary>
    /// Extensions for Http interactions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds <see cref="IGithubHttp"/> to Di
        /// </summary>
        public static IServiceCollection AddGithubHttp(this IServiceCollection services, IConfiguration configuration)
        {
            var githubConfigration = configuration.GetSection("Http:Github").CheckExistence();

            services.AddHttpClient<IGithubHttp, GithubHttp>();
            services.Configure<GithubHttpConfiguration>(githubConfigration);

            return services;
        }
    }
}
