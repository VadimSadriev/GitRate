using GitRate.Common.Types;

namespace GitRate.Common.Http.Clients.Github
{
    /// <summary>
    /// Configuration for <see cref="GithubHttp"/>
    /// </summary>
    public class GithubHttpConfiguration : IConiguration
    {
        /// <summary>
        /// Url for git hub api
        /// </summary>
        public string Url { get; set; }
    }
}
