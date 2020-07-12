using FluentAssertions;
using GitRate.Common.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace GitRate.Common.Tests
{
    /// <summary>
    /// Unit tests for http things
    /// </summary>
    public class HttpTests
    {
        private Http.Http _sut;
        private Mock<ILogger<Http.Http>> _loggerMock = new Mock<ILogger<Http.Http>>();
        private Mock<ILogger<JsonHttp>> _jsonLoggerMock = new Mock<ILogger<JsonHttp>>();

        public HttpTests()
        {

        }

        public class Post
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }
        }

        [Fact]
        public async Task Should_Get()
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
    }
}
