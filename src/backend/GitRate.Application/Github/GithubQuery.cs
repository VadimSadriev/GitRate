namespace GitRate.Application.Github
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
        public GithubQuery(string query)
        {
            _queryString = query;
        }

        public static GithubQuery operator &(GithubQuery query1, GithubQuery query2)
        {
            return new GithubQuery($"{query1._queryString}&{query2._queryString}");
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
