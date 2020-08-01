using System.Runtime.Serialization;

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
        [EnumMember(Value ="unknown")]
        Unknown = 0,

        /// <summary>
        /// Criteria to search inside description, readme, etc
        /// </summary>
        [EnumMember(Value = "in")]
        In = 1,

        /// <summary>
        /// Creatia to search by user
        /// </summary>
        [EnumMember(Value = "user")]
        User = 2,

        /// <summary>
        /// Ctiretia to searcg by organization
        /// </summary>
        [EnumMember(Value = "org")]
        Org = 3,

        /// <summary>
        /// Ctiretia to search by size
        /// </summary>
        [EnumMember(Value = "size")]
        Size = 4,

        /// <summary>
        /// Criteria to search by followers
        /// </summary>
        [EnumMember(Value = "followers")]
        Followers = 5,

        /// <summary>
        /// Criteria to search by forks
        /// </summary>
        [EnumMember(Value = "forks")]
        Forks = 6,

        /// <summary>
        /// Criteria to search by stars
        /// </summary>
        [EnumMember(Value = "stars")]
        Stars = 7,

        /// <summary>
        /// Criteria to search by creation date
        /// </summary>
        [EnumMember(Value = "created")]
        Created = 8,

        /// <summary>
        /// Criteria to search by last updated
        /// </summary>
        [EnumMember(Value = "pushed")]
        Pushed = 9,

        /// <summary>
        /// Criteria to search by language
        /// </summary>
        [EnumMember(Value = "language")]
        Language = 10,

        /// <summary>
        /// Criteria to search by topic
        /// </summary>
        [EnumMember(Value = "topic")]
        Topic = 11,

        /// <summary>
        /// Criteria to searcg by public or private repository you have acces to
        /// </summary>
        [EnumMember(Value = "is")]
        Is = 12,

        /// <summary>
        /// Criteria to search by archive repo or not
        /// </summary>
        [EnumMember(Value = "archived")]
        Archived = 13,

        /// <summary>
        /// Criteria to search based on numbr of issues
        /// </summary>
        [EnumMember(Value = "good-first-issues")]
        GoodFirstIssues = 14,

        /// <summary>
        /// Criteria to search repos with number of repos marked as Help wanted
        /// </summary>
        [EnumMember(Value = "help-wanted-issues")]
        HelpWantedIssues = 15
    }
}
