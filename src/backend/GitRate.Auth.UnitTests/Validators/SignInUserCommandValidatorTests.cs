using System.Linq;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Validators;
using FluentAssertions;
using GitRate.Auth.UnitTests.Base;
using Xunit;

namespace GitRate.Auth.UnitTests.Validators
{
    public class SignInUserCommandValidatorTests : TestBase
    {
        private readonly SignInUserCommandValidator _sut;

        public SignInUserCommandValidatorTests()
        {
            _sut = new SignInUserCommandValidator();
        }

        [Fact]
        public async Task Should_Validate()
        {
            // arrange
            var command = new SignInUserCommand
            {
                UserNameOrEmail = "Alice",
                Password = "Alice12345"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().BeEmpty();
        }
        
        [Fact]
        public async Task Should_Not_Validate_With_Empty_UserNameOrEmail()
        {
            // arrange
            var command = new SignInUserCommand
            {
                UserNameOrEmail = "",
                Password = "Alice12345"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            
            result.Errors.Select(x => x.ErrorMessage).Should().Contain($"UserName or Email cannot be empty.");
        }
    }
}