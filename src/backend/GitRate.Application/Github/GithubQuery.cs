﻿namespace GitRate.Application.Github
{
    /// <summary>
    /// Contains data for querying github recourses
    /// </summary>
    public class GithubQuery
    {
        private string _queryString;

        /// <summary>
        /// Contains data for querying github recourses
        /// </summary>
        public GithubQuery(string name)
        {
            _queryString = name;
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