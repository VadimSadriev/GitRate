namespace GitRate.Application.Enums
{
    /// <summary>
    /// Search criteria to concrete search
    /// for example /repositories?q=git-rate+user:Alice
    /// which means search repos wich has user Alice
    /// </summary>
    public enum RepositorySearchCriteria
    {
        /// <summary>
        /// Criteria is not defined
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Criteria to search inside description, readme, etc
        /// </summary>
        In = 1,

        /// <summary>
        /// Creatia to search by user
        /// </summary>
        User = 2,

        /// <summary>
        /// Ctiretia to searcg by organization
        /// </summary>
        Org = 3,

        /// <summary>
        /// Ctiretia to search by size
        /// </summary>
        Size = 4,

        /// <summary>
        /// Criteria to search by followers
        /// </summary>
        Followers = 5,

        /// <summary>
        /// Criteria to search by forks
        /// </summary>
        Forks = 6,

        /// <summary>
        /// Criteria to search by stars
        /// </summary>
        Stars = 7,

        /// <summary>
        /// Criteria to search by creation date
        /// </summary>
        Created = 8,

        /// <summary>
        /// Criteria to search by last updated
        /// </summary>
        Pushed = 9,

        /// <summary>
        /// Criteria to search by language
        /// </summary>
        Language = 10,

        /// <summary>
        /// Criteria to search by topic
        /// </summary>
        Topic = 11,

        /// <summary>
        /// Criteria to searcg by public or private repository you have acces to
        /// </summary>
        Is = 12,

        /// <summary>
        /// Criteria to search by archive repo or not
        /// </summary>
        Archived = 13,

        /// <summary>
        /// Criteria to search based on numbr of issues
        /// </summary>
        GoodFirstIssues = 14,

        /// <summary>
        /// Criteria to search repos with number of repos marked as Help wanted
        /// </summary>
        HelpWantedIssues = 15
    }
}
