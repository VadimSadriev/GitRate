namespace GitRate.Application.Dto
{
    /// <summary>
    /// Contains data to search github resources
    /// </summary>
    public abstract class GithubSearchDto
    {
        /// <summary>
        /// Keyword to search
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Flag if search should find only on resource
        /// </summary>
        public bool IsSingle { get; set; }
    }
}
