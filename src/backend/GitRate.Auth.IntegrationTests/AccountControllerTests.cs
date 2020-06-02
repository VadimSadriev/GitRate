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

        [Theory]
        [InlineData("", "Alice@gmail.com", "Alice12345")]
        [InlineData("Alice", "", "Alice12345")]
        [InlineData("Alice", "Alice@gmail.com", "")]
        public async Task SignUp_Should_Fail_With_Empty_Data(string userName, string email, string password)
        {
            // arrange 
            var signUpUserCommand = new SignUpUserCommand
            {
                UserName = userName,
                Email = email,
                Password = password
            };
            
            // act
            var response = await TestClient.PostAsJsonAsync(_signUpUri, signUpUserCommand);

            // assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            response.Content.Should().NotBeNull();
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var responseModel = await response.Content.ReadAsAsync<ExceptionContract>();

            responseModel.Should().NotBeNull();
            responseModel.Errors.Should().NotBeNullOrEmpty();
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

            // assert
            signInResponse.Should().NotBeNull();
            signInResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            signInResponse.Content.Should().NotBeNull();
            signInResponse.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var signInResponseModel = await signInResponse.Content.ReadAsAsync<SignInUserResultDto>();

            signInResponseModel.Should().NotBeNull();
            signInResponseModel.Jwt.Should().NotBeNullOrEmpty();
            signInResponseModel.RefreshToken.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("", "Alice12345")]
        [InlineData("Alice", "")]
        public async Task SignIn_Should_Fail_With_Empty_Data(string userNameOrEmail, string password)
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
                UserNameOrEmail = userNameOrEmail,
                Password = password
            };

            // act
            var signInResponse = await TestClient.PostAsJsonAsync(_signInUri, signInUserCommand);

            // assert
            signInResponse.Should().NotBeNull();
            signInResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            signInResponse.Content.Should().NotBeNull();
            signInResponse.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);

            var signInResponseModel = await signInResponse.Content.ReadAsAsync<ExceptionContract>();

            signInResponseModel.Should().NotBeNull();
            signInResponseModel.Should().NotBeNull();
            signInResponseModel.Errors.Should().NotBeNullOrEmpty();
        }
    }
}