using GitRate.Common.Http.Clients.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitRate.Common.Http.Clients.Github
{
    /// <summary>
    /// Http to interact with github
    /// </summary>
    public class GithubHttp : JsonHttp, IGithubHttp
    {
        private readonly IOptions<GithubHttpConfiguration> _options;

        /// <summary>
        /// Http to interact with github
        /// </summary>
        public GithubHttp(
            HttpClient httpClient,
            ILogger<GithubHttp> logger,
            IOptions<GithubHttpConfiguration> options
            )
            : base(httpClient, logger)
        {
            _options = options;
            httpClient.BaseAddress = new Uri(_options.Value.Url, UriKind.Absolute);
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("git-rate")));
        }
    }
}
