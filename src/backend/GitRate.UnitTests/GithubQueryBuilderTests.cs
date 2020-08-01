using FluentAssertions;
using GitRate.Application.Dto.Repository;
using GitRate.Application.Enums;
using GitRate.Application.Extensions;
using GitRate.Application.Github;
using GitRate.Application.Services;
using GitRate.Common.Http.Clients.Github;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GitRate.UnitTests
{
    public class GithubQueryBuilderTests
    {
        private readonly GithubQueryBuilder _sut;
        private readonly Mock<IOptions<GithubHttpConfiguration>> _githubOptions = new Mock<IOptions<GithubHttpConfiguration>>();


        public GithubQueryBuilderTests()
        {
            _sut = new GithubQueryBuilder(_githubOptions.Object);
        }

        [Fact]
        public void Should_Build_With_One_Keyword()
        {
            // arrange
            var githubApi = "https://api.github.com/search/repositories";

            // act
            var query = GithubSpecifications.WithKeyword("git-rate");

            var resultQuery = $"{githubApi}?{query}";

            // assert
            resultQuery.ToString().Should().Be($"{githubApi}?q=git-rate");
        }

        [Fact]
        public void Should_Build_With_Multiple_Keywords()
        {
            // arrange
            var githubApi = "https://api.github.com/search/repositories";

            var keyWords = new[] { "git-rate", "aspnet", "wpf" };

            // act
            var query = GithubSpecifications.WithKeywords(keyWords);

            var resultQuery = $"{githubApi}?{query}";

            // assert
            resultQuery.ToString().Should().Be($"{githubApi}?q=git-rate+aspnet+wpf");
        }

        [Fact]
        public void Should_Build_With_Multiple_Queries()
        {
            // arrange
            var query = GithubSpecifications.WithKeyword("git-rate");

            // act
            var resultQuery = query
                .WithSort("stars")
                .WithOrder("asc");

            // assert
            resultQuery.ToString().Should().Be($"q=git-rate&sort=stars&order=asc");
        }


        [Fact]
        public void Should_Build_With_Criteria()
        {
            // arrange
            var query = GithubSpecifications.WithKeyword("git-rate");

            // act
            var resultQuery = query
                .WithCriteria("user:Alice");

            // assert
            resultQuery.ToString().Should().Be($"q=git-rate+user:Alice");
        }

        [Fact]
        public void Should_Build_Repository_Search_Query_From_Dto()
        {
            // arrange
            var dto = new GithubRepositorySearchDto
            {
                Keywords = new[] { "git-rate", "aspnet" },
                Criterias = new[]
                {
                    new GithubRepositorySearchCriteriaDto
                    {
                        Criteria = RepositorySearchCriteria.In,
                        Values = new [] { "description", "readme" }
                    },
                    new GithubRepositorySearchCriteriaDto
                    {
                        Criteria = RepositorySearchCriteria.User,
                        Values = new [] { "Octat" }
                    }
                },
                Order = "desc",
                Sort = "stars"
            };

            _githubOptions.Setup(x => x.Value)
                .Returns(new GithubHttpConfiguration
                {
                    Url = "https://api.github.com"
                });

            // act
            var result = _sut.BuildRepositorySearch(dto);

            // assert
            result.Should().Be("https://api.github.com/search/repositories?q=git-rate+aspnet+in:description,readme+user:Octat&order=desc&sort=stars");
        }
    }
}
