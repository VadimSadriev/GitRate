using FluentAssertions;
using GitRate.Application.Github;
using GitRate.Common.Extensions;
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
    }
}
