using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using FluentAssertions;
using GitRate.Auth.IntegrationTests.Base;
using GitRate.Web.Common.Contracts.Exception;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace GitRate.Auth.IntegrationTests
{
    public class AccountControllerTests : BaseTest
    {
        private static string _baseUri = "api/account";
        private static string _signUpUri = _baseUri + "/signup";
        private static string _signInUri = _baseUri + "/signin";

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
            var response = await TestClient.PostAsJsonAsync(_signUpUri, signUpUserCommand);

            // assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Content.Should().NotBeNull();
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var responseModel = await response.Content.ReadAsAsync<SignUpUserResultDto>();

            responseModel.Should().NotBeNull();
            responseModel.Jwt.Should().NotBeNullOrEmpty();
            responseModel.RefreshToken.Should().NotBeNullOrEmpty();
        }
        
        [Fact]
        public async Task Should_SignIn()
        {
            // arrange
            var signUpUserCommand = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@gmail.com",
                Password = "Alice12345"
            };
            
            var signUpResponse = await TestClient.PostAsJsonAsync(_signUpUri, signUpUserCommand);
            
            signUpResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            signUpResponse.Content.Should().NotBeNull();
            signUpResponse.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var signInUserCommand = new SignInUserCommand
            {
                UserNameOrEmail = "Alice",
                Password = "Alice12345"
            };

            // act
            var signInResponse = await TestClient.PostAsJsonAsync(_signInUri, signInUserCommand);

            signInResponse.Should().NotBeNull();
            signInResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            signInResponse.Content.Should().NotBeNull();
            signInResponse.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var signInResponseModel = await signInResponse.Content.ReadAsAsync<SignInUserResultDto>();

            signInResponseModel.Should().NotBeNull();
            signInResponseModel.Jwt.Should().NotBeNullOrEmpty();
            signInResponseModel.RefreshToken.Should().NotBeNullOrEmpty();
        }
    }
}