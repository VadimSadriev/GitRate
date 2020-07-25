namespace GitRate.Application.Github
{
    /// <summary>
    /// Specifications for github queries
    /// </summary>
    public static class GithubSpecifications
    {
        /// <summary>
        /// Returns string with required query parameter
        /// </summary>
        /// <param name="keyword">Keyword to search</param>
        public static GithubQuery WithKeyword(string keyword) => new GithubQuery($"q={keyword}");
    }
}
