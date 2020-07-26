namespace GitRate.Application.Enums
{
    /// <summary>
    /// Defines resources to search
    /// </summary>
    public enum SearchResource
    {
        /// <summary>
        /// Resource is not defined
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Search github repositories
        /// </summary>
        Repositories = 1,

        /// <summary>
        /// Search github codes
        /// </summary>
        Code = 2,

        /// <summary>
        /// Search github commits
        /// </summary>
        Commits = 3,

        /// <summary>
        /// Search github issues and pull requests
        /// </summary>
        Issues,

        /// <summary>
        /// Search github users
        /// </summary>
        Users,

        /// <summary>
        /// Seach github topics
        /// </summary>
        Topics
    }
}
