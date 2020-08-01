using System;

namespace GitRate.Application.Github
{
    /// <summary>
    /// Contains data for querying github recourses
    /// </summary>
    public class GithubQuery
    {
        protected string _queryString;

        /// <summary>
        /// Contains data for querying github recourses
        /// </summary>
        public GithubQuery(string query)
        {
            _queryString = query;
        }

        public static GithubQuery operator &(GithubQuery query1, GithubQuery query2)
        {
            var query = new GithubQuery($"{query1._queryString}&{query2._queryString}");

            return query;
        }

        /// <summary>
        /// Adds criteria to query to concrete search result
        /// </summary>
        public GithubQuery WithCriteria(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
                throw new ArgumentNullException(nameof(criteria), "Criteria for github query cannot be null or empty");

            return new GithubQuery($"{_queryString}+{criteria}");
        }

        /// <summary>
        /// String representation of this query
        /// </summary>
        public override string ToString()
        {
            return _queryString;
        }
    }
}
