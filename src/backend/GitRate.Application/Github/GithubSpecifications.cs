namespace GitRate.Application.Github
{
    /// <summary>
    /// Specifications for github queries
    /// </summary>
    public static class GithubSpecifications
    {
        /// <summary>
        /// Returns query with required query parameter
        /// </summary>
        public static GithubQuery WithKeyword(string keyword) => new GithubQuery($"q={keyword}");

        /// <summary>
        /// Returns query with sorting
        /// </summary>
        public static GithubQuery WithSort(string sortWord) => new GithubQuery($"sort={sortWord}");

        /// <summary>
        /// Returns query with order
        /// </summary>
        public static GithubQuery WithOrder(string order) => new GithubQuery($"order={order}");
    }
}
