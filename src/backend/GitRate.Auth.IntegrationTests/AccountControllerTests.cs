using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using FluentAssertions;
using GitRate.Auth.IntegrationTests.Base;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GitRate.Auth.IntegrationTests
{
    public class AccountControllerTests : BaseTest
    {
        private static string _baseUri = "api/account";
        private static string _signupUri = _baseUri + "/signup";

        [Fact]
        public async Task Should_SignUp()
        {
            // arrange 
            var signUpUserCommand = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@gmail.com",
                Password = "Alice12345"
            };
            
            // act
            var response = await TestClient.PostAsJsonAsync(_signupUri, signUpUserCommand);

            // assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Content.Should().NotBeNull();
            response.Content.Headers.ContentType.Should().Be(MediaTypeNames.Application.Json);

            var responseModel = await response.Content.ReadAsAsync<SignUpUserResultDto>();

            responseModel.Should().NotBeNull();
            responseModel.Jwt.Should().NotBeNull();
            responseModel.RefreshToken.Should().NotBeNull();
        }
    }
}