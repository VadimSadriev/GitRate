using FluentAssertions;
using GitRate.Application.Github;
using GitRate.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using Xunit;

namespace GitRate.UnitTests
{
    public class GithubQueryBuilderTests
    {
        [Fact]
        public void Should_Build_With_One_Keyword()
        {
            var githubApi = "https://api.github.com/search/repositories";

            var query = GithubSpecifications.WithKeyword("git-rate");

            var resultQuery = $"{githubApi}?{query}";

            resultQuery.ToString().Should().Be($"{githubApi}?q=git-rate");
        }

        [Fact]
        public void Should_Build_With_Multiple_Queries()
        {
            var query = GithubSpecifications.WithKeyword("git-rate");

            var sortQuery = new GithubQuery("sort=stars");

            var orderQuery = new GithubQuery("order=asc");

            var resultQuery = query & sortQuery & orderQuery;

            resultQuery.ToString().Should().Be($"q=git-rate&sort=stars&order=asc");
        }
    }
}
