using System.Linq;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Validators;
using FluentAssertions;
using GitRate.Auth.Domain;
using GitRate.Auth.UnitTests.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GitRate.Auth.UnitTests.Validators
{
    public class SignUpUserCommandValidatorTests : TestBase
    {
        private readonly SignUpUserCommandValidator _sut;
        private readonly Mock<IOptions<IdentityOptions>> _identityOptionsMock = new Mock<IOptions<IdentityOptions>>();
        
        public SignUpUserCommandValidatorTests()
        {
            _identityOptionsMock.Setup(x => x.Value)
                .Returns(new IdentityOptions
                {
                    Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequiredLength = 6,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequiredUniqueChars = 0,
                        RequireNonAlphanumeric = false
                    }
                });
            
            _sut = new SignUpUserCommandValidator(_context, _identityOptionsMock.Object);
        }

        [Fact]
        public async Task Should_Be_Valid()
        {
            // arrange
            var command = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@gmail.com",
                Password = "Alice12345"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task Should_Be_Invalid_Email()
        {
            // arrange
            var command = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@!gmail.com",
                Password = "Alice12345"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain($"Email: {command.Email} is not in correct format.");
        }
        
        [Fact]
        public async Task Should_Be_Invalid_UserName()
        {
            // arrange
            var command = new SignUpUserCommand
            {
                UserName = "",
                Email = "Alice@gmail.com",
                Password = "Alice12345"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("UserName cannot be empty.");
        }
        
        [Fact]
        public async Task Should_Be_Invalid_Password()
        {
            // arrange
            var command = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@!gmail.com",
                Password = "Ali"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain($"Password must be at least 6 characters.");
        }

        [Fact]
        public async Task Should_Not_Validate_With_Existing_UserName()
        {
            // arrane
            _context.Users.Add(new User { NormalizedUserName = "ALICE" });
            _context.SaveChanges();
            
            var command = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@!gmail.com",
                Password = "Alice"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain($"UserName: {command.UserName} is already taken.");
        }
        
        [Fact]
        public async Task Should_Not_Validate_With_Existing_Email()
        {
            // arrane
            _context.Users.Add(new User { NormalizedEmail = "ALICE@GMAIL.COM" });
            _context.SaveChanges();
            
            var command = new SignUpUserCommand
            {
                UserName = "Alice",
                Email = "Alice@gmail.com",
                Password = "Alice"
            };
            
            // act
            var result = _sut.Validate(command);
            
            // assert
            result.Errors.Should().NotBeNullOrEmpty();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain($"Email: {command.Email} is already taken.");
        }
    }
}