using GitRate.Application.Github;

namespace GitRate.Application.Extensions
{
    /// <summary>
    /// Extensions for <see cref="GithubQuery"/>
    /// </summary>
    public static class GithubQueryExtensions
    {
        /// <summary>
        /// Adds sorting query parameter to <paramref name="query"/>
        /// </summary>
        public static GithubQuery WithSort(this GithubQuery query, string sort)
        {
            return query & GithubSpecifications.WithSort(sort);
        }

        /// <summary>
        /// Adds order query parameter to <paramref name="query"/>
        /// </summary>
        public static GithubQuery WithOrder(this GithubQuery query, string order)
        {
            return query & GithubSpecifications.WithOrder(order);
        }
    }
}
