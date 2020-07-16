using FluentAssertions;
using GitRate.Common.Http.Clients.Github;
using GitRate.Common.Http.Clients.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace GitRate.Common.IntegrationTests
{
    /// <summary>
    /// Unit tests for http things
    /// </summary>
    public class HttpTests
    {
        private Http.Http _sut;
        private Mock<ILogger<Http.Http>> _loggerMock = new Mock<ILogger<Http.Http>>();
        private Mock<ILogger<JsonHttp>> _jsonLoggerMock = new Mock<ILogger<JsonHttp>>();
        private Mock<ILogger<GithubHttp>> _githubLoggerMock = new Mock<ILogger<GithubHttp>>();
        private Mock<IOptions<GithubHttpConfiguration>> _githubOptionsMock = new Mock<IOptions<GithubHttpConfiguration>>();

        public HttpTests()
        {

        }

        [Fact]
        public async Task Should_Get_Json()
        {
            // arrange
            var uri = "https://my-json-server.typicode.com/typicode/demo/posts";
            var method = HttpMethod.Get;
            var client = new HttpClient();

            _sut = new JsonHttp(client, _jsonLoggerMock.Object);

            // act 
            var response = await _sut
                .For(uri)
                .SendAsync<List<Post>>(method);

            // assert
            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
            response.Should().HaveCount(3);
        }

        [Fact]
        public async Task Should_Get_Repos_From_Github()
        {
            // arrange
            var url = "https://api.github.com/";
            var uri = "search/users?q=vadimsadriev&order=asc";
            var method = HttpMethod.Get;
            var client = new HttpClient();
            _githubOptionsMock.Setup(x => x.Value)
                .Returns(new GithubHttpConfiguration
                {
                    Url = url
                });

            _sut = new GithubHttp(client, _githubLoggerMock.Object, _githubOptionsMock.Object);

            // act
            var response = await _sut
                .For(uri)
                .GetAsync<GitHubResult>();

            // assert
            response.Should().NotBeNull();
            response.TotalCount.Should().Be(1);
            response.Items.Should().HaveCount(1);
            response.Items.Should().Contain(x => x.Login == "VadimSadriev");
        }

        private class Post
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        private class GitHubResult
        {
            [JsonPropertyName("total_count")]
            public long TotalCount { get; set; }

            [JsonPropertyName("incomplete_results")]
            public bool IncompleteResults { get; set; }

            [JsonPropertyName("items")]
            public List<GithubAccount> Items { get; set; }
        }

        private class GithubAccount
        {
            [JsonPropertyName("login")]
            public string Login { get; set; }

            [JsonPropertyName("id")]
            public long Id { get; set; }

            [JsonPropertyName("node_id")]
            public string NodeId { get; set; }

            [JsonPropertyName("avatar_url")]
            public string AvatarUrl { get; set; }

            [JsonPropertyName("gravatar_id")]
            public string GravatarId { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
