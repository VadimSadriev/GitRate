using System.Collections;
using System.Collections.Generic;

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

        public static GithubQuery WithKeywords(IEnumerable<string> keywords) => new GithubQuery($"q={string.Join("+", keywords)}");

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
